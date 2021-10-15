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
        [HttpGet]
        public List<YouTubeVideo> Get()
        {
            return new VideoController().GetAllVideos(_dbManager);               
        }
        
        [HttpGet("search")]
        public IActionResult GetVideo(string videoId)
        {
            YouTubeVideo video = new VideoController().GetVideoById(_dbManager, videoId);

            if(video == null)
            {
                return NoContent();
            }

            return Ok(video);      
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
