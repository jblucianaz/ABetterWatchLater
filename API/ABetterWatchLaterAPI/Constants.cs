public static class Constants
{
    public static class YouTube
    {
        public const string API_KEY = Secret.API_KEY;
        public const string BASE_YT_URL = "https://www.youtube.com/watch?v=";
        public const string BASE_API_URL = "https://youtube.googleapis.com/youtube/v3/";
        public const string CHANNELS = "channels";
        public const string VIDEOS = "videos";    
    }

    public static class ThumbnailSize
    {
        // Video:120x90 Channel:88x88
        public const string DEFAULT = "default";
        // Video:320x180 Channel:240x240
        public const string MEDIUM = "medium";
        // Video:480x360 Channel:800x800
        public const string HIGH = "high";
        // Video:640x480
        public const string STANDARD = "standard";
        // Video:1280x720
        public const string MAXRES = "maxres";
    }

    public static class PropertiesName
    {
        public const string CHANNEL_ID = "channelId";
        public const string CONTENT_DETAILS = "contentDetails";
        public const string DURATION = "duration";
        public const string ID = "id";
        public const string ITEMS = "items";
        public const string SNIPPET = "snippet";
        public const string TAGS = "tags";
        public const string THUMBNAILS = "thumbnails";
        public const string TITLE = "title";
        public const string URL = "url";       
    }

    public static class QueryTypes
    {
        public const string BY_CHANNEL_NAME = "By Channel Name";
        public const string BY_VIDEO_TITLE = "By Video Title";
        public const string MULTI_SEARCH = "Multi Search";
    }
}