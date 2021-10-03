using System;
using System.Collections.Generic;
using System.Text.Json;
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

        private DbManager GetServiceForDbManager()
        {
            return HttpContext.RequestServices.GetService(typeof(ABetterWatchLaterAPI.Models.DbManager)) as DbManager;
        }

        [HttpGet]
        public List<YouTubeVideo> Get()
        {
            DbManager dbManager = GetServiceForDbManager();

            List<YouTubeVideo> results = dbManager.GetAllVideos();

            return results;
        }

        [HttpGet("search")]
        public IActionResult GetVideo(string videoId)
        {
            DbManager dbManager = GetServiceForDbManager();

            YouTubeVideo video = dbManager.GetVideoById(videoId);

            return Ok(video);
        }

        [HttpPost]
        public IActionResult AddVideo([FromBody] string videoId)
        {
            YouTubeController ytc = new YouTubeController();
            DbManager dbManager = GetServiceForDbManager();

            using (JsonDocument document = JsonDocument.Parse(videoId))
            {
                JsonElement root = document.RootElement;
                foreach (JsonElement id in root.EnumerateArray())
                {
                    string jsonResult = ytc.GetVideoInfo(id.ToString());

                    YouTubeVideo video = ytc.ConvertJsonToYoutubeVideo(jsonResult);
                    dbManager.AddVideo(video);

                    if (!dbManager.IsChannelInDatabase(video.ChannelId))
                    {
                        string jsonResult2 = ytc.GetChannelInfo(video.ChannelId);
                        dbManager.AddChannel(ytc.ConvertJsonToYouTubeChannel(jsonResult2));
                    }
                }
            }
           
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteVideo(string videoId)
        {
            YouTubeController ytc = new YouTubeController();
            DbManager dbManager = GetServiceForDbManager();
            dbManager.DeleteVideo(videoId);

            string jsonResult = ytc.GetVideoInfo(videoId);
            YouTubeVideo video = ytc.ConvertJsonToYoutubeVideo(jsonResult);

            if (dbManager.IsChannelInDatabase(video.ChannelId))  // TODO: Need to check if there is more video with this channel ID before deleting it completely.
            {
                dbManager.DeleteChannel(video.ChannelId);
            }

            return Ok();
        }
    }
}
