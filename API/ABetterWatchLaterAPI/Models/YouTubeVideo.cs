using System;

namespace ABetterWatchLaterAPI
{
    public class YouTubeVideo
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Url => Constants.YouTube.BASE_YT_URL + Id;

        public string ChannelId { get; set; }

        public string Duration { get; set; }

        public string[] Tags { get; set; };
    }
}
