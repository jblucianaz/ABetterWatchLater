using System;

namespace ABetterWatchLaterAPI
{
    public class YouTubeChannel
    {
        public string ChannelId { get; set; }

        public string Title { get; set; }

        public string Thumbnail { get; set; }

        public YouTubeChannel(string channelId, string title, string thumbnail)
        {
            ChannelId = channelId;
            Title = title;
            Thumbnail = thumbnail;
        }
    }
}
