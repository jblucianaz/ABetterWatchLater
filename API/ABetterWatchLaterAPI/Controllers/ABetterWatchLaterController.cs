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

        private DbManager _dbManager
        {
            get
            { 
                return HttpContext.RequestServices.GetService(typeof(DbManager)) as DbManager;
            }
        }

        #region Routes

        #region HttpGet
        [HttpGet("videos")]
        public List<YouTubeVideo> GetAllVideos()
        {
            return new VideoController().GetAllVideos(_dbManager);
        }

        [HttpGet("channels")]
        public List<YouTubeChannel> GetAllChannels()
        {
            return new ChannelController().GetAllChannels(_dbManager);
        }

        [HttpGet("search")]
        public IActionResult SearchVideos(string videoTitle, string channelName)
        {
            string query = string.Empty;
            string queryType = string.Empty;

            if (videoTitle != null)
            {
                query = videoTitle;
                queryType = Constants.QueryTypes.BY_VIDEO_TITLE;
            }

            if (channelName != null)
            {
                query = channelName;
                queryType = Constants.QueryTypes.BY_CHANNEL_NAME;
            }

            if ((channelName != null) && (videoTitle != null))
            {
                queryType = Constants.QueryTypes.MULTI_SEARCH;
            }    

            List<YouTubeVideo> videos = new VideoController().SearchVideos(_dbManager, query, queryType);

            if (videos == null)
            {
                return NoContent();
            }

            return Ok(videos);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public IActionResult AddVideo([FromBody] string[] videoIds)
        {
            try
            {
                new VideoController().AddVideos(_dbManager, videoIds);
                return Ok($"{videoIds.Count()} elements added to database.");
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion

        #region HttpDelete
        [HttpDelete]
        public IActionResult DeleteVideo(string videoId)
        {
            try
            {
                new VideoController().DeleteVideo(_dbManager, videoId);
                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion

        #endregion
    }
}
