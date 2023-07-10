﻿using ProjectVideoGameV2.POCO;
using ProjetVideoGameV2.Model.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;

namespace ProjetVideoGameV2.Model.Dao
{
    internal class VideoGamesDAO : DAO<VideoGames>
    {
        public VideoGamesDAO(){ }

        public override bool Create(VideoGames obj)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.VideoGame(name, creditCost, console) VALUES('{obj.Name}','{null}','{obj.Console}')", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }

        public override bool Delete(VideoGames obj)
        {
            return false;
        }
        public override bool Update(VideoGames obj)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.VideoGame SET creditCost = '{obj.CreditCost}' WHERE idVideoGame = @id", connection);
                cmd.Parameters.AddWithValue("id", obj.IdVideoGames);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }
        public override VideoGames Find(int id)
        {

            VideoGames videoGames = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))

                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame WHERE idVideoGame = @id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            videoGames = new VideoGames
                            {
                                IdVideoGames = reader.GetInt32("idVideoGame"),
                                Name = reader.GetString("name"),
                                CreditCost = reader.GetInt32("creditCost"),
                                Console = reader.GetString("console"),
                                BookingList = new List<Booking>(),
                                CopyList  = new List<Copy>()
                            };
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return videoGames;
        }

        
        public override List<VideoGames> FindAll()
        {
            List<VideoGames> videoGames = new List<VideoGames>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGames vg = new VideoGames();
                        vg.IdVideoGames = reader.GetInt32("idVideoGame");
                        vg.Name = reader.GetString("name");
                        vg.CreditCost = reader.GetInt32("creditCost");
                        vg.Console = reader.GetString("console");
                        if(vg.CreditCost > 0)
                        {
                            videoGames.Add(vg);
                        }
                    }
                }
            }
            return videoGames;

        }

        public List<VideoGames> GetVideoGamesAdmin()
        {
            List<VideoGames> videoGames = new List<VideoGames>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGames vg = new VideoGames();
                        vg.IdVideoGames = reader.GetInt32("idVideoGame");
                        vg.Name = reader.GetString("name");
                        vg.CreditCost = reader.GetInt32("creditCost");
                        vg.Console = reader.GetString("console");
                        if (vg.CreditCost == 0)
                        {
                            videoGames.Add(vg);
                        }
                    }
                }
            }
            return videoGames;

        }

        public List<VideoGames> GetVideoGamesByName(string name)
        {
            List<VideoGames> videoGames = new List<VideoGames>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame WHERE name = @name", connection);
                cmd.Parameters.AddWithValue("name", name);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGames vg = new VideoGames();
                        vg.IdVideoGames = reader.GetInt32("idVideoGame");
                        vg.Name = reader.GetString("name");
                        vg.CreditCost = reader.GetInt32("creditCost");
                        vg.Console = reader.GetString("console");
                        videoGames.Add(vg);
                    }
                }
            }
            return videoGames;

        }

        public bool CopyAvailable(int id)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE idVideoGame = @id", connection);
                cmd.Parameters.AddWithValue("id", id);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

    }
}
