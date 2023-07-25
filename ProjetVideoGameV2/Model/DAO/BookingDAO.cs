﻿using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Booking (bookingDate, idVideoGame, idUser) VALUES ('{formattedDateNow}', '{obj.VideoGames.IdVideoGames}', '{obj.Player.IdPlayer}')", connection);
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
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Booking SET bookingDate = '{obj.BookingDate}' WHERE idBooking = @idBooking", connection);
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
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking WHERE idBooking = @id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            booking = new Booking();
                            {
                                booking.Idbooking = reader.GetInt32("idBooking");
                                booking.BookingDate = reader.GetDateTime("bookingDate");
                                booking.VideoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                                booking.Player.IdPlayer = reader.GetInt32("idPlayer");
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
                        booking.VideoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                        booking.Player.IdPlayer = reader.GetInt32("idPlayer");
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;

        }
    }
}
