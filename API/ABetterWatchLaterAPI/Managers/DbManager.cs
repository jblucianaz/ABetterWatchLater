using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using ABetterWatchLaterAPI.Models;

namespace ABetterWatchLaterAPI.Managers
{
    public class DbManager
    {
        public string ConnectionString { get; set; }

        public DbManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        #region Videos
        public List<YouTubeVideo> GetAllVideos()
        {
            List<YouTubeVideo> list = new List<YouTubeVideo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Video", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new YouTubeVideo(
                            reader["VideoId"].ToString(),
                            reader["Title"].ToString(),
                            reader["ChannelId"].ToString(),
                            reader["Duration"].ToString(),
                            reader["Tags"].ToString().Split('.').ToList(),
                            reader["Thumbnail"].ToString()));
                    }
                }
            }
            return list;
        }

        public YouTubeVideo GetVideoById(string videoId)
        {
            YouTubeVideo video = new YouTubeVideo();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Video WHERE", conn);
                cmd.CommandText =
                    "SELECT * FROM Video WHERE VideoId = @videoId";

                cmd.Parameters.Add(new MySqlParameter("videoId", videoId));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        video.VideoId = reader["VideoId"].ToString();
                        video.Title = reader["Title"].ToString();
                        video.ChannelId = reader["ChannelId"].ToString();
                        video.Duration = reader["Duration"].ToString();
                        video.Tags = reader["Tags"].ToString().Split('.').ToList();
                        video.Thumbnail = reader["Thumbnail"].ToString();
                    }
                }
            }

            if(!video.isValid())
            {
                return null;
            }

            return video;
        }

        public List<YouTubeVideo> SearchVideosByTitle(string name)
        {
            List<YouTubeVideo> list = new List<YouTubeVideo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Video WHERE Title LIKE '%%'", conn);
                cmd.CommandText = "SELECT * FROM Video WHERE Title LIKE @title";
                cmd.Parameters.Add(new MySqlParameter("title", $"%{name}%"));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new YouTubeVideo(
                            reader["VideoId"].ToString(),
                            reader["Title"].ToString(),
                            reader["ChannelId"].ToString(),
                            reader["Duration"].ToString(),
                            reader["Tags"].ToString().Split('.').ToList(),
                            reader["Thumbnail"].ToString()));
                    }
                }
            }
            return list;
        }

        public List<YouTubeVideo> SearchVideosChannelId(string channelId)
        {
            List<YouTubeVideo> list = new List<YouTubeVideo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Video WHERE ChannelId = '", conn);
                cmd.CommandText = "SELECT * FROM Video WHERE ChannelId LIKE @channelId";
                cmd.Parameters.Add(new MySqlParameter("channelId", channelId));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new YouTubeVideo(
                            reader["VideoId"].ToString(),
                            reader["Title"].ToString(),
                            reader["ChannelId"].ToString(),
                            reader["Duration"].ToString(),
                            reader["Tags"].ToString().Split('.').ToList(),
                            reader["Thumbnail"].ToString()));
                    }
                }
            }
            return list;
        }

        public void AddVideo(YouTubeVideo video)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO () VALUES ()", conn);
                cmd.CommandText =
                    "INSERT INTO Video (VideoId, Title, Url, ChannelId, Duration, Tags, Thumbnail) VALUES (@videoId, @title, @url, @channelId, @duration, @tags, @thumbnail)";
                
                cmd.Parameters.Add(new MySqlParameter("videoId", video.VideoId));
                cmd.Parameters.Add(new MySqlParameter("title", video.Title));
                cmd.Parameters.Add(new MySqlParameter("url", video.Url));
                cmd.Parameters.Add(new MySqlParameter("channelId", video.ChannelId));
                cmd.Parameters.Add(new MySqlParameter("duration", video.Duration));
                cmd.Parameters.Add(new MySqlParameter("tags", video.TagsAsString()));
                cmd.Parameters.Add(new MySqlParameter("thumbnail", video.Thumbnail));


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }
            }
        }

        public void DeleteVideo(string videoId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Video WHERE", conn);
                cmd.CommandText =
                    "DELETE FROM Video WHERE VideoId = @videoId";

                cmd.Parameters.Add(new MySqlParameter("videoId", videoId));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }
            }
        }
        #endregion

        #region Channels
        public List<YouTubeChannel> GetAllChannels()
        {
            List<YouTubeChannel> list = new List<YouTubeChannel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Channel", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new YouTubeChannel(
                            reader["ChannelId"].ToString(),
                            reader["Name"].ToString(),
                            reader["Thumbnail"].ToString()));
                    }
                }
            }
            return list;
        }

        public YouTubeChannel GetChannelById(string channelId)
        {
            YouTubeChannel channel = new YouTubeChannel();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Channel WHERE", conn);
                cmd.CommandText =
                    "SELECT * FROM Channel WHERE ChannelId = @channelId";

                cmd.Parameters.Add(new MySqlParameter("channelId", channelId));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        channel.ChannelId = reader["ChannelId"].ToString();
                        channel.Name = reader["Name"].ToString();
                        channel.Thumbnail = reader["Thumbnail"].ToString();
                    }
                }
            }

            return channel;
        }

        public void AddChannel(YouTubeChannel channel)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO () VALUES ()", conn);
                cmd.CommandText =
                    "INSERT INTO Channel (ChannelId, Name, Thumbnail) VALUES (@channelId, @name, @thumbnail)";

                cmd.Parameters.Add(new MySqlParameter("channelId", channel.ChannelId));
                cmd.Parameters.Add(new MySqlParameter("name", channel.Name));
                cmd.Parameters.Add(new MySqlParameter("thumbnail", channel.Thumbnail));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }
            }
        }

        public void DeleteChannel(string channelId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Channel WHERE", conn);
                cmd.CommandText =
                    "DELETE FROM Channel WHERE ChannelId = @channelId";

                cmd.Parameters.Add(new MySqlParameter("channelId", channelId));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }
            }
        }

        public bool IsChannelInDatabase(string channelId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM Channel WHERE", conn);
                cmd.CommandText =
                    "SELECT COUNT(*) FROM Channel WHERE ChannelId = @channelId";

                cmd.Parameters.Add(new MySqlParameter("channelId", channelId));

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    int entries = Convert.ToInt32(result);

                    return (entries > 0);
                }
                else
                {
                    throw new Exception("Error");
                }
            }
        }

        public int CountVideosFromChannel(string channelId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM Channel WHERE", conn);
                cmd.CommandText =
                    "SELECT COUNT(*) FROM Video WHERE ChannelId = @channelId";

                cmd.Parameters.Add(new MySqlParameter("channelId", channelId));

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    throw new Exception("Error");
                }
            }
        }
        #endregion
    }
}
