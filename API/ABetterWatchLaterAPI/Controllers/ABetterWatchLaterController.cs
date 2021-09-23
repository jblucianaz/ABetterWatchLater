using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ABetterWatchLaterAPI.Models;

namespace ABetterWatchLaterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ABetterWatchLaterController : ControllerBase
    {
        private readonly ILogger<ABetterWatchLaterController> _logger;

        public ABetterWatchLaterController(ILogger<ABetterWatchLaterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            YouTubeController ytc = new YouTubeController();
            DbManager dbManager = HttpContext.RequestServices.GetService(typeof(ABetterWatchLaterAPI.Models.DbManager)) as DbManager;
            
            List<YouTubeVideo> results = dbManager.GetAllVideos();

            YouTubeVideo video = results[0];
            
            return video.ToFakeJson();
        }

        [HttpPost]
        public IActionResult AddVideo(string videoId)
        {
            YouTubeController ytc = new YouTubeController();
            DbManager dbManager = HttpContext.RequestServices.GetService(typeof(ABetterWatchLaterAPI.Models.DbManager)) as DbManager;

            string jsonResult = Task<string>.Run(() => {
                return ytc.GetVideoInfo(videoId);
            }).Result;

            dbManager.AddVideo(ytc.ConvertJsonToYoutubeVideo(jsonResult));

            return Ok();
        }
    }
}
