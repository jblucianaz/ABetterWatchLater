using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ABetterWatchLaterAPI.Managers;
using ABetterWatchLaterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ABetterWatchLaterAPI.Controllers
{
    public class VideoController
    {
        public List<YouTubeVideo> GetAllVideos(DbManager dbManager)
        {
            return dbManager.GetAllVideos();
        }

        public YouTubeVideo GetVideoById(DbManager dbManager, string videoId)
        {
             return dbManager.GetVideoById(videoId);
        }

        public List<YouTubeVideo> SearchVideos(DbManager dbManager, string query, string queryType)
        {
            if (queryType == Constants.QueryTypes.BY_VIDEO_TITLE) { return dbManager.SearchVideosByTitle(query); }
            if (queryType == Constants.QueryTypes.BY_CHANNEL_NAME) { return dbManager.SearchVideosByChannelName(query); }
            if (queryType == Constants.QueryTypes.MULTI_SEARCH) { return dbManager.SearchVideosByTitle(query); }

            return new List<YouTubeVideo>();
        }

        public void AddVideo(DbManager dbManager, string videoId)
        {
            YouTubeVideo video = new YouTubeController().GetVideo(videoId);
            YouTubeController youtubeController = new YouTubeController();

            dbManager.AddVideo(video);

            if (!dbManager.IsChannelInDatabase(video.ChannelId))
            {
                YouTubeChannel channel = youtubeController.GetChannel(video.ChannelId);
                dbManager.AddChannel(channel);
            }
        }

        public void AddVideos(DbManager dbManager, string[] videoIds)
        {
            if (videoIds.Count() == 1)
            {
                AddVideo(dbManager, videoIds[0]);
                return;
            }

            foreach (string id in videoIds)
            {
                AddVideo(dbManager, id);
            }
        }

        public void DeleteVideo(DbManager dbManager, string videoId)
        {
            YouTubeVideo videoToDelete = new YouTubeController().GetVideo(videoId);
            dbManager.DeleteVideo(videoId);

            if (dbManager.CountVideosFromChannel(videoToDelete.ChannelId) == 0)
            {
                dbManager.DeleteChannel(videoToDelete.ChannelId);
            }
        }
    }
}