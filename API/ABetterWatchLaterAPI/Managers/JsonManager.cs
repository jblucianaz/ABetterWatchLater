using System;
using System.Collections.Generic;
using System.Text.Json;
using ABetterWatchLaterAPI.Models;

namespace ABetterWatchLaterAPI.Managers
{
    public class JsonManager
    {
        public JsonManager()
        {
        }

        #region Get Elements
        /// <summary>
        /// Get the root element of a Json document.
        /// </summary>
        /// <param name="jsonString">The Json document as a string.</param>
        /// <returns>The root element as a JsonElement.</returns>
        public JsonElement GetRootElement(string jsonString)
        {
            JsonDocument document = JsonDocument.Parse(jsonString);
            return document.RootElement;

        }

        /// <summary>
        /// Get a child JsonElement by a property name.
        /// </summary>
        /// <param name="jsonElement">The parent element to explore.</param>
        /// <param name="propertyName">The searched property.</param>
        /// <returns>A child JsonElement matching the property name.</returns>
        public JsonElement GetElements(JsonElement jsonElement, string propertyName)
        {
            if (jsonElement.TryGetProperty(propertyName, out JsonElement childElement))
            {
                return childElement;
            }
            else
            {
                throw new Exception($"Property not found: {propertyName}");
            }
        }

        /// <summary>
        /// Get tags from a video.
        /// </summary>
        /// <param name="jsonElement">The parent element to explore.</param>
        /// <returns>A list of tags.</returns>
        public List<string> GetTags(JsonElement jsonElement)
        {
            List<string> tags = new List<string>();

            if ((jsonElement.TryGetProperty(Constants.PropertiesName.TAGS, out JsonElement tagsElement)))
            {
                foreach (JsonElement tag in tagsElement.EnumerateArray())
                {
                    tags.Add(tag.GetString());
                }
            }

            return tags;
        }

        /// <summary>
        /// Get thumbnail from a video.
        /// </summary>
        /// <param name="element">The parent element to explore.</param>
        /// <param name="size">The size of the thumbnail.</param>
        /// <returns>The link to the thumbnail.</returns>
        public string GetThumbnail(JsonElement element, string size)
        {
            return element
                .GetProperty(Constants.PropertiesName.THUMBNAILS)
                .GetProperty(size)
                .GetProperty(Constants.PropertiesName.URL)
                .ToString();
        }
        #endregion

        #region Conversion to object
        /// <summary>
        /// Get a Json string from YouTube API 
        /// to make a YouTube video object.
        /// </summary>
        /// <param name="jsonString">The Json document as a string.</param>
        /// <returns>A YouTube video object.</returns>
        public YouTubeVideo ConvertJsonToYoutubeVideo(string jsonString)
        {
            string videoId = string.Empty;
            string title = string.Empty;
            string channelId = string.Empty;
            string duration = string.Empty;
            List<string> tags = new List<string>();
            string thumbnail = string.Empty;

            JsonElement root = GetRootElement(jsonString);

            foreach (JsonElement item in GetElements(root, Constants.PropertiesName.ITEMS).EnumerateArray())
            {
                videoId = GetElements(item, Constants.PropertiesName.ID).ToString();

                JsonElement snippetElement = GetElements(item, Constants.PropertiesName.SNIPPET);
                title = GetElements(snippetElement, Constants.PropertiesName.TITLE).ToString();
                channelId = GetElements(snippetElement, Constants.PropertiesName.CHANNEL_ID).ToString();

                tags = GetTags(snippetElement);

                thumbnail = GetThumbnail(snippetElement, Constants.ThumbnailSize.STANDARD);

                JsonElement contentDetailsElement = GetElements(item, Constants.PropertiesName.CONTENT_DETAILS);
                duration = GetElements(contentDetailsElement, Constants.PropertiesName.DURATION).ToString();
            }

            YouTubeVideo youtubeVideo = new YouTubeVideo(videoId, title, channelId, duration, tags, thumbnail);

            return youtubeVideo;
        }

        /// <summary>
        /// Get a Json string from YouTube API 
        /// to make a YouTube channel object.
        /// </summary>
        /// <param name="jsonString">The Json document as a string.</param>
        /// <returns>A YouTube channel object.</returns>
        public YouTubeChannel ConvertJsonToYouTubeChannel(string jsonString)
        {
            string channelId = string.Empty;
            string name = string.Empty;
            string thumbnail = string.Empty;

            JsonElement root = GetRootElement(jsonString);

            foreach (JsonElement item in GetElements(root, Constants.PropertiesName.ITEMS).EnumerateArray())
            {
                channelId = GetElements(item, Constants.PropertiesName.ID).ToString();

                JsonElement snippetElement = GetElements(item, Constants.PropertiesName.SNIPPET);
                name = snippetElement.GetProperty(Constants.PropertiesName.TITLE).ToString();
                thumbnail = GetThumbnail(snippetElement, Constants.ThumbnailSize.MEDIUM);
            }

            YouTubeChannel youTubeChannel = new YouTubeChannel(channelId, name, thumbnail);

            return youTubeChannel;
        }
        #endregion
    }
}
