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
        public List<YouTubeVideo> Get()
        {
            DbManager dbManager = HttpContext.RequestServices.GetService(typeof(ABetterWatchLaterAPI.Models.DbManager)) as DbManager;
            
            List<YouTubeVideo> results = dbManager.GetAllVideos();

            return results;
        }

        [HttpGet("search")]
        public IActionResult GetVideo(string videoId)
        {
            DbManager dbManager = HttpContext.RequestServices.GetService(typeof(ABetterWatchLaterAPI.Models.DbManager)) as DbManager;
            YouTubeVideo video = dbManager.GetVideoById(videoId);

            return Ok(video);
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
