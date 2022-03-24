using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
    

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration config;
        private readonly IOptions<MySettings> appsettings;//store as it is
        private readonly MySettings mySettings;//strip it of IOptions interface

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config,IOptions<MySettings> appsettings)
        {
            _logger = logger;
            this.config = config;
            this.appsettings = appsettings;
            this.mySettings = appsettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            //var openWeatherUrl = config.GetSection("MySettings").GetSection("OpenWeatherMapUrl").Value;
            //var apiKey = config.GetSection("MySettings").GetSection("ApiKey").Value;

            var openWeatherUrl = config.GetValue<string>("MySettings:OpenWeatherMapUrl");
            var apiKey = config.GetValue<string>("MySettings:ApiKey");

            HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync($"{openWeatherUrl}{apiKey}");
            HttpResponseMessage response = await client.GetAsync($"{mySettings.OpenWeatherMapUrl}{mySettings.ApiKey}");
            var contentData = string.Empty;
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //chestii
                contentData = response.Content.ReadAsStringAsync().Result;
            }
            return Ok(contentData);
        }

    }
}
