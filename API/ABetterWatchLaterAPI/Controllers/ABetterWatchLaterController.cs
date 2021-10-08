using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ABetterWatchLaterAPI.Models;
using ABetterWatchLaterAPI.Managers;

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
            return HttpContext.RequestServices.GetService(typeof(DbManager)) as DbManager;
        }

        [HttpGet]
        public List<YouTubeVideo> Get()
        {
            DbManager dbManager = GetServiceForDbManager();
            return dbManager.GetAllVideos();
                
        }

        [HttpGet("search")]
        public IActionResult GetVideo(string videoId)
        {
            DbManager dbManager = GetServiceForDbManager();
            YouTubeVideo video = dbManager.GetVideoById(videoId);

            if(video == null)
            {
                return NoContent();
            }

            return Ok(video);      
        }

        [HttpPost]
        public IActionResult AddVideo([FromBody] string videoIds)
        {
            YouTubeController ytc = new YouTubeController();
            DbManager dbManager = GetServiceForDbManager();

            try
            {
                using (JsonDocument document = JsonDocument.Parse(videoIds))
                {
                    JsonElement root = document.RootElement;
                    foreach (JsonElement id in root.EnumerateArray())
                    {
                        YouTubeVideo video = ytc.GetVideo(id.ToString());
                        dbManager.AddVideo(video);

                        if (!dbManager.IsChannelInDatabase(video.ChannelId))
                        {
                            YouTubeChannel channel = ytc.GetChannel(video.ChannelId);
                            dbManager.AddChannel(channel);
                        }
                    }
                }

                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteVideo(string videoId)
        {
            DbManager dbManager = GetServiceForDbManager();
            dbManager.DeleteVideo(videoId);

            YouTubeController ytc = new YouTubeController();
            YouTubeVideo video = ytc.GetVideo(videoId);

            if (dbManager.IsChannelInDatabase(video.ChannelId))  // TODO: Need to check if there is more video with this channel ID before deleting it completely.
            {
                dbManager.DeleteChannel(video.ChannelId);
            }

            return Ok();
        }
    }
}
