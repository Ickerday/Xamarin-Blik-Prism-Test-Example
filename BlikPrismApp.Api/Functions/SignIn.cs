using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace BlikPrismApp.Api.Functions
{
    public static class SignIn
    {
        [FunctionName(nameof(SignIn))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Post), Route = "signin")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Function {nameof(SignIn)} triggered");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string username = data?.username;
            string password = data?.password;

            log.LogInformation($"Signing in user {username}");

            return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password)
                ? (ActionResult)new OkObjectResult($"Hello, {username}")
                : new BadRequestObjectResult($"Please provide {nameof(username)} and {nameof(password)}");
        }
    }
}
