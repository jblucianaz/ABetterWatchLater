using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ABetterWatchLaterAPI.Managers;
using ABetterWatchLaterAPI.Models;

namespace ABetterWatchLaterAPI.Controllers
{
    public class YouTubeController
    {   
        /// <summary>
        /// Create the URL to fetch data from the YouTube API.
        /// </summary>
        /// <param name="queryType">The kind of object we get info about: video or channel.</param>
        /// <param name="id">The ID of the video or channel.</param>
        /// <returns>A complete and valid YouTube API URL.</returns>
        private string CreateGetURL(string queryType, string id)
        {
            return Constants.YouTube.BASE_API_URL + queryType + "?part=snippet" + 
                (queryType == Constants.YouTube.VIDEOS ? "&part=contentDetails" : "") + 
                "&key=" + Constants.YouTube.API_KEY +
                "&id=" + id;
        }

        /// <summary>
        /// Fetch data from the YouTube API.
        /// </summary>
        /// <param name="url">The URL to the YouTube API.</param>
        /// <returns>A A task to get all datas about the requested object.</returns>
        private async Task<string> CallYouTubeApi(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        /// <summary>
        /// Run the task fetching data from the YouTube API.
        /// </summary>
        /// <param name="url">The URL to the YouTube API.</param>
        /// <returns>A Json string with all datas about the requested object.</returns>
        private string RunCallYouTubeApiTask(string url)
        {
            return Task.Run(() => {
                return CallYouTubeApi(url);
            }).Result;
        }

        /// <summary>
        /// Get video data from an ID. 
        /// </summary>
        /// <param name="videoId">The ID of the video.</param>
        /// <returns>Video data as Json.</returns>
        private string GetVideoInfo(string videoId)
        {
            return RunCallYouTubeApiTask(CreateGetURL(Constants.YouTube.VIDEOS, videoId));
        }

        /// <summary>
        /// Get channel data from an ID. 
        /// </summary>
        /// <param name="channelId">The ID of the video.</param>
        /// <returns>Channel data as Json.</returns>
        private string GetChannelInfo(string channelId)
        {
            return RunCallYouTubeApiTask(CreateGetURL(Constants.YouTube.CHANNELS, channelId));
        }

        /// <summary>
        /// Get video object from an ID. 
        /// </summary>
        /// <param name="channelId">The ID of the video.</param>
        /// <returns>YouTube video as an object.</returns>
        public YouTubeVideo GetVideo(string videoId)
        {
            return new JsonManager().ConvertJsonToYoutubeVideo(GetVideoInfo(videoId));
        }

        /// <summary>
        /// Get channel object from an ID. 
        /// </summary>
        /// <param name="channelId">The ID of the channel.</param>
        /// <returns>YouTube channel data as an object.</returns>
        public YouTubeChannel GetChannel(string channelId)
        {
            return new JsonManager().ConvertJsonToYouTubeChannel(GetChannelInfo(channelId));
        }
    }
}