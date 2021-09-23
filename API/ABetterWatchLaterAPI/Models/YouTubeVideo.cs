using System;
using System.Collections.Generic;
using System.Linq;

namespace ABetterWatchLaterAPI
{
    public class YouTubeVideo
    {
        public string VideoId { get; set; }

        public string Title { get; set; }

        public string Url => Constants.YouTube.BASE_YT_URL + VideoId;

        public string ChannelId { get; set; }

        public string Duration { get; set; }

        public List<string> Tags { get; set; }

        public string Thumbnail { get; set; }

        public YouTubeVideo(string videoId, string title, string channelId, string duration, List<string> tags, string thumbnail)
        {
            VideoId = videoId;
            Title = title;
            ChannelId = channelId;
            Duration = duration;
            Tags = tags;
            Thumbnail = thumbnail;
        }

        public string ToFakeJson()
        {
            return @$"
            VideoId: {VideoId},
            Title: {Title},
            ChannelId: {ChannelId},
            Duration: {Duration},
            Tags: {TagsAsString()},
            Thumbnail: {Thumbnail}";
        }

        public string TagsAsString()
        {
            string strTags = string.Empty;

            foreach (string tag in Tags)
            {
                if (tag != "") 
                    strTags += $"{tag},";
            }

            return strTags.Remove(strTags.Length-1);
        }
    }
}
