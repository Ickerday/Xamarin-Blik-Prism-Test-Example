using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlikPrismApp.Api.Functions
{
    public static class GetBlikCode
    {
        [FunctionName(nameof(GetBlikCode))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Post), Route = "blik")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Function {nameof(GetBlikCode)} triggered");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string username = data?.username;

            log.LogInformation($"Getting BLIK code for user {username}");

            return !string.IsNullOrWhiteSpace(username)
                ? (ActionResult)new OkObjectResult(new Random().Next(minValue: 100000, maxValue: 1000000)) // 6 number value
                : new BadRequestResult();
        }
    }
}
