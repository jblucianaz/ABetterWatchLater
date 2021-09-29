using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ABetterWatchLaterAPI.Controllers
{
    public class YouTubeController : ControllerBase
    {   
        public string CreateGetURL(string queryType, string id)
        {
            return Constants.YouTube.BASE_API_URL + queryType + "?part=snippet" + 
                (queryType == Constants.YouTube.VIDEOS ? "&part=contentDetails" : "") + 
                "&key=" + Constants.YouTube.API_KEY +
                "&id=" + id;
        }

        public async Task<string> GetVideoInfo(string videoId)
        {
            string url = CreateGetURL(Constants.YouTube.VIDEOS, videoId);
            string videoInfo;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    videoInfo = apiResponse;
                }
            }
            return videoInfo;
        }

        public YouTubeVideo ConvertJsonToYoutubeVideo(string jsonString) 
        {
            string videoId = string.Empty;
            string title = string.Empty;
            string channelId = string.Empty;
            string duration = string.Empty;
            List<string> tags = new List<string>();
            string thumbnail = string.Empty;
            
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                JsonElement itemElements = root.GetProperty("items");

                foreach (JsonElement item in itemElements.EnumerateArray())
                {
                    if (item.TryGetProperty("id", out JsonElement idElement))
                    {
                        videoId = idElement.ToString();
                    }
                    if (item.TryGetProperty("snippet", out JsonElement snippetElement))
                    {
                        title = snippetElement.GetProperty("title").ToString();
                        channelId = snippetElement.GetProperty("channelId").ToString();

                        if (snippetElement.TryGetProperty("tags", out JsonElement tagsElement))
                        {
                            foreach (JsonElement tag in tagsElement.EnumerateArray())
                            {
                                tags.Add(tag.GetString());
                            }
                        }
                        thumbnail = snippetElement
                            .GetProperty("thumbnails")
                            .GetProperty("standard")
                            .GetProperty("url").ToString();
                    }
                    if (item.TryGetProperty("contentDetails", out JsonElement contentDetailsElement))
                    {
                        duration = contentDetailsElement.GetProperty("duration").ToString();
                    }
                }
            }
            
            YouTubeVideo youtubeVideo = new YouTubeVideo(videoId, title, channelId, duration, tags, thumbnail);
            
            return youtubeVideo;
        }
    }
}