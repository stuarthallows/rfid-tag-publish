using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Somark.Models;

namespace Somark
{
    public static class Functions
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "serverless")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("tag-scanned")]
        public static async Task<IActionResult> TagScanned(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] TagScannedRequest request,
            [SignalR(HubName = "serverless")] IAsyncCollector<SignalRMessage> signalRMessages,
            ExecutionContext context,
            ILogger log)
        {
            // TODO add validation for tag format
            log.LogInformation("{FunctionName} invoked with tag {TagId}", context.FunctionName, request.TagId);

            var message = new SignalRMessage
                {
                    Target = "nextTag",
                    Arguments = new object[] { request.TagId }
                };
            await signalRMessages.AddAsync(message);

            return new OkResult();
        }
    }
}
