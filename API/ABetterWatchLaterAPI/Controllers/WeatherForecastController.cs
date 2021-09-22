using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ABetterWatchLaterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public static string[] Summaries1 => Summaries;

        [HttpGet]
        public string Get()
        {
            YouTubeController ytc = new YouTubeController();
            string jsonResult = Task<string>.Run( () => {
                return ytc.GetVideoInfo("Ks-_Mh1QhMc");
            }).Result;

            YouTubeVideo video = ytc.ConvertJsonToYoutubeVideo(jsonResult);

            //return jsonResult;
            return video.ToFakeJson();
        }
    }
}
