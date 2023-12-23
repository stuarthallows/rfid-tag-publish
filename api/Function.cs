using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;

namespace CSharp
{
    public static class Function
    {
        [FunctionName("index")]
        public static IActionResult GetHomePage([HttpTrigger(AuthorizationLevel.Anonymous)]HttpRequest req, ExecutionContext context)
        {
            var path = Path.Combine(context.FunctionAppDirectory, "content", "index.html");
            return new ContentResult
            {
                Content = File.ReadAllText(path),
                ContentType = "text/html",
            };
        }

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "serverless")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("broadcast")]
        public static async Task Broadcast([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
                                            [SignalR(HubName = "serverless")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            Random random = new();

            var nextNumber = random.Next(1, 1000000);

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "nextNumber",
                    Arguments = new[] { $"The next random number is: {nextNumber}" }
                });
        }

        // [FunctionName("broadcastTimer")]
        // public static async Task BroadcastTimer([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer,
        //                                     [SignalR(HubName = "serverless")] IAsyncCollector<SignalRMessage> signalRMessages)
        // {
        //     var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/azure/azure-signalr");
        //     request.Headers.UserAgent.ParseAdd("Serverless");
        //     request.Headers.Add("If-None-Match", Etag);
        //     var response = await httpClient.SendAsync(request);
        //     if (response.Headers.Contains("Etag"))
        //     {
        //         Etag = response.Headers.GetValues("Etag").First();
        //     }
        //     if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //     {
        //         var result = JsonConvert.DeserializeObject<GitResult>(await response.Content.ReadAsStringAsync());
        //         StarCount = result.StarCount;
        //     }

        //     await signalRMessages.AddAsync(
        //         new SignalRMessage
        //         {
        //             Target = "newMessage",
        //             Arguments = new[] { $"Current star count of https://github.com/Azure/azure-signalr is: {StarCount}" }
        //         });
        // }
    }
}