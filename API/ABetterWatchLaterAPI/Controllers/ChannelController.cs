using ABetterWatchLaterAPI.Models;
using ABetterWatchLaterAPI.Managers;
using System;
using System.Collections.Generic;

namespace ABetterWatchLaterAPI.Controllers
{
    public class ChannelController
    {
        public List<YouTubeChannel> GetAllChannels(DbManager dbManager)
        {
            return dbManager.GetAllChannels();
        }
    }
}