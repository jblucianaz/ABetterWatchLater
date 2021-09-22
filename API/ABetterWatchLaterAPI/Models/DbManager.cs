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
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Video WHERE id < 10", conn);

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
    }
}
