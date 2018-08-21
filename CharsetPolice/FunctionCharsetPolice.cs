
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using CharsetPolice.Police;
using System;
using System.Threading.Tasks;

namespace CharsetPolice
{
    public static class FunctionCharsetPolice
    {
        [FunctionName("CharsetPolice")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "charset-police")]CharsetPoliceRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"Request URI: {req.Uri}");

            var policeMan = new CharsetPoliceMan();
            var result = await policeMan.SearchAsync(new Uri(req.Uri));

            return new OkObjectResult(result);
        }

        public class CharsetPoliceRequest
        {
            [Required]
            public string Uri { get; set; }
        }
    }
}
