using System;
using System.Collections.Generic;

namespace ABetterWatchLaterAPI
{
    public class YouTubeVideo
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Url => Constants.YouTube.BASE_YT_URL + Id;

        public string ChannelId { get; set; }

        public string Duration { get; set; }

        public List<string> Tags { get; set; }

        public YouTubeVideo(string id, string title, string channelId, string duration, List<string> tags ) 
        {
            Id = id;
            Title = title;
            ChannelId = channelId;
            Duration = duration;
            Tags = tags;
        }
    }
}
