using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Common;

namespace Api
{
    public static class GetWeatherForecast
    {
        private static HttpClient httpClient = new HttpClient();

        [FunctionName("forecast")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
            ILogger log)
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Blazor Forecast");
            var response = await httpClient.GetStringAsync("https://api.weather.gov/zones/public/WVZ516/forecast");
            var forecasts = JsonConvert.DeserializeObject<NwsForecast>(response);
            var result = forecasts.properties.periods
                .Where(f => f.number % 2 == 1)
                .Select((f, i) => new WeatherForecast
                {
                    Date = DateTime.UtcNow.Date.AddDays(i),
                    Summary = f.detailedForecast
                });
            return new OkObjectResult(result);
        }
    }
}
