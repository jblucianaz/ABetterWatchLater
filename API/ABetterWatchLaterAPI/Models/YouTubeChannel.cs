using System;

namespace ABetterWatchLaterAPI
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
    }
}
