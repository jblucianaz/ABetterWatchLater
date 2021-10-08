using System;

namespace ABetterWatchLaterAPI.Models
{
    public class YouTubeChannel
    {
        public string ChannelId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public YouTubeChannel(string channelId, string name, string thumbnail)
        {
            ChannelId = channelId;
            Name = name;
            Thumbnail = thumbnail;
        }

        public YouTubeChannel()
        {
        }
    }
}
