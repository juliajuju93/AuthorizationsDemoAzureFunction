using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace FunctionAppAuth.Function
{
    public class FunctionAppTrigger
    {
        [FunctionName("FunctionAppTrigger")]
        public async Task Run([TimerTrigger("*/1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Azure Function started at: {DateTime.Now}");

            //GET to get the count of issues
            var baseurl = System.Environment.GetEnvironmentVariable("CUSTOM_URL", EnvironmentVariableTarget.Process);
            var subscriptionKey = System.Environment.GetEnvironmentVariable("SUBSCRIPTION_KEY", EnvironmentVariableTarget.Process);
            var addurl = "?state=open";
            var _baseurl = baseurl + addurl;
            log.LogInformation($"URL: {_baseurl}");

            //Define GET header
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            //GET call
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _baseurl);
            var response = await client.SendAsync(request);
            JArray arr = JArray.Parse(await response.Content.ReadAsStringAsync());

            //Error handeling
            log.LogInformation($"You currently have {arr.Count} issues open.");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Call unsuccessful: {response.IsSuccessStatusCode}");
            };

            //POST to teams the number of issues
            var teamsurl = System.Environment.GetEnvironmentVariable("TEAMS_URL", EnvironmentVariableTarget.Process);
            log.LogInformation($"URL: {teamsurl}");

            // Create TeamsBody
            var TeamsBody = new
            { 
                body = new
                {
                    content = $"You currently have {arr.Count} issues open."
                }
            };
            log.LogInformation($"Body: {TeamsBody.body.content}");

            //Define POST header
            HttpClient clientteams = new HttpClient();
            clientteams.DefaultRequestHeaders.Accept.Clear();
            clientteams.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            //POST call with TeamsBody content
            HttpRequestMessage requestteams = new HttpRequestMessage(HttpMethod.Post, teamsurl);
            requestteams.Content = new ObjectContent<object>(TeamsBody, new JsonMediaTypeFormatter());
            var responseteams = await clientteams.SendAsync(requestteams);

            //Error handeling
            if (!responseteams.IsSuccessStatusCode)
            {
                throw new Exception($"Call unsuccessful: {responseteams.IsSuccessStatusCode}");
            };
        }
    }

    internal class Body
    {
        public string Content { get; set; }
    }

}
