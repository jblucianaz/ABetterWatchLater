using System;
using System.Collections.Generic;

namespace ABetterWatchLaterAPI
{
    public class YouTubeVideo
    {
        //public string Id { get; set; }

        public string VideoId { get; set; }

        public string Title { get; set; }

        public string Url => Constants.YouTube.BASE_YT_URL + VideoId;

        public string ChannelId { get; set; }

        public string Duration { get; set; }

        public List<string> Tags { get; set; }

        public string Thumbnail { get; set; }

        public YouTubeVideo(string videoId, string title, string channelId, string duration, List<string> tags, string thumbnail)
        {
            //Id = id;
            VideoId = videoId;
            Title = title;
            ChannelId = channelId;
            Duration = duration;
            Tags = tags;
            Thumbnail = thumbnail;
        }
    }
}
