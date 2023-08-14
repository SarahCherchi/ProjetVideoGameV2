using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjetVideoGameV2.Model.DAO
{
    internal class BookingDAO : DAO<Booking>
    {
        public BookingDAO() { }

        public override bool Create(Booking obj)
        {
            bool success = false;
            string formattedDateNow = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Booking (bookingDate, idVideoGame, idUser, numberOfWeeks) VALUES ('{formattedDateNow}', '{obj.VideoGames.IdVideoGames}', '{obj.Player.IdPlayer}', '{obj.NumberOfWeeks}')", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }


        public override bool Delete(int id)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Booking WHERE idBooking = @id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
                return success;
            }
            return success;
        }

        public override bool Update(Booking obj)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Booking SET numberOfWeeks = '{obj.NumberOfWeeks}' WHERE idBooking = @idBooking", connection);
                cmd.Parameters.AddWithValue("idBooking", obj.Idbooking);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }

        public override Booking Find(int id)
        {
            Booking booking = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))

                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking WHERE idBooking = @idBooking", connection);
                    cmd.Parameters.AddWithValue("idBooking", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            booking = new Booking();
                            {
                                booking.Idbooking = reader.GetInt32("idBooking");
                                booking.BookingDate = reader.GetDateTime("bookingDate");
                                VideoGames videoGames = new VideoGames();
                                videoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                                booking.VideoGames = videoGames;
                                Player player = new Player();
                                player.IdPlayer = reader.GetInt32("idUser");
                                booking.Player = player;
                                booking.NumberOfWeeks = reader.GetInt32("numberOfWeeks");
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return booking;
        }

        public override List<Booking> FindAll()
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking();
                        booking.Idbooking = reader.GetInt32("idBooking");
                        booking.BookingDate = reader.GetDateTime("bookingDate");
                        VideoGames videoGames = new VideoGames();
                        videoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                        booking.VideoGames = videoGames;
                        Player player = new Player();
                        player.IdPlayer = reader.GetInt32("idUser");
                        booking.Player = player;
                        booking.NumberOfWeeks = reader.GetInt32("numberOfWeeks");
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;

        }

        public bool DeleteByIdUserAndIdVideoGames(int idUser, int idVideoGame)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Booking WHERE idVideoGame = @idVideoGame AND idUser = @idUser", connection);
                    cmd.Parameters.AddWithValue("idVideoGame", idVideoGame);
                    cmd.Parameters.AddWithValue("idUser", idUser);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return success;
        }

        public Booking FindByVideoGameAndUser(int idUser, int idVideoGame)
        {
            Booking booking = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))

                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking WHERE idVideoGame = @idVideoGame AND idUser = @idUser", connection);
                    cmd.Parameters.AddWithValue("idVideoGame", idVideoGame);
                    cmd.Parameters.AddWithValue("idUser", idUser);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            booking = new Booking();
                            {
                                booking.Idbooking = reader.GetInt32("idBooking");
                                booking.BookingDate = reader.GetDateTime("bookingDate");
                                VideoGames videoGames = new VideoGames();
                                videoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                                booking.VideoGames = videoGames;
                                Player player = new Player();
                                player.IdPlayer = reader.GetInt32("idUser");
                                booking.Player = player;
                                booking.NumberOfWeeks = reader.GetInt32("numberOfWeeks");
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return booking;
        }

        public List<Booking> FindAllByIdVidegoGame(int id)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking WHERE idVideoGame = @idVideoGame", connection);
                cmd.Parameters.AddWithValue("idVideoGame", id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking();
                        booking.Idbooking = reader.GetInt32("idBooking");
                        booking.BookingDate = reader.GetDateTime("bookingDate");
                        VideoGames videoGames = new VideoGames();
                        videoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                        booking.VideoGames = videoGames;
                        Player player = new Player();
                        player.IdPlayer = reader.GetInt32("idUser");
                        booking.Player = player;
                        booking.NumberOfWeeks = reader.GetInt32("numberOfWeeks");
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }

        public int CountMemberOnWaitingList(int id)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking WHERE idVideoGame = @idVideoGame", connection);
                cmd.Parameters.AddWithValue("idVideoGame", id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking();
                        booking.Idbooking = reader.GetInt32("idBooking");
                        booking.BookingDate = reader.GetDateTime("bookingDate");
                        VideoGames videoGames = new VideoGames();
                        videoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                        booking.VideoGames = videoGames;
                        Player player = new Player();
                        player.IdPlayer = reader.GetInt32("idUser");
                        booking.Player = player;
                        booking.NumberOfWeeks = reader.GetInt32("numberOfWeeks");
                        bookings.Add(booking);
                    }
                }
            }
            return bookings.Count;
        }
    }
}

