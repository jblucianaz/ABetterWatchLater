using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace ABetterWatchLaterAPI.Models
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

            return video;
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
    }
}
