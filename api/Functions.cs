using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace TagPublish;

public record TagRequest(string TagId);

public class Functions
{
    private readonly IConfiguration _configuration;

    public Functions(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [FunctionName("negotiate")]
    public static SignalRConnectionInfo Negotiate(
        [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
        [SignalRConnectionInfo(HubName = "serverless")] SignalRConnectionInfo connectionInfo)
    {
        return connectionInfo;
    }

    [FunctionName("tag-scanned")]
    public async Task<IActionResult> TagScanned(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        [SignalR(HubName = "serverless")] IAsyncCollector<SignalRMessage> signalRMessages,
        ExecutionContext context,
        ILogger log)
    {
        if (!IsAuthenticated(req))
        {
            log.LogWarning("Unauthorized call made to ${FunctionName}", context.FunctionName);
            return new UnauthorizedResult();
        }
                
        var request = await JsonSerializer.DeserializeAsync<TagRequest>(req.Body);
        
        if (request.TagId.Length > 40)
        {
            log.LogWarning("{FunctionName} invoked with invalid tag", context.FunctionName);
            return new BadRequestResult();
        }
        
        log.LogInformation("{FunctionName} invoked with tag {TagId}", context.FunctionName, request.TagId);

        var message = new SignalRMessage
            {
                Target = "nextTag",
                Arguments = new object[] { request.TagId }
            };
        await signalRMessages.AddAsync(message);

        return new OkResult();
    }

    private bool IsAuthenticated(HttpRequest req)
    {
        return !string.IsNullOrEmpty(_configuration["XFunctionsKey"]) &&
             (req.Headers["XFunctionsKey"].SingleOrDefault()?.Equals(_configuration["XFunctionsKey"]) ?? false);
    }
}
