using ProjetVideoGameV2.Model.DAO;
using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;

namespace ProjetVideoGameV2.Model.Dao
{
    internal class PlayerDAO : DAO<Player>
    {
        public PlayerDAO(){ }

        public override bool Create(Player obj)
        {
            bool success = false;
            string formattedDateOfBirth = obj.DateOfBirth.ToString("yyyy-MM-dd");
            string formattedDateNow = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            DateTime ldb = DateTime.MinValue;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.[User] (username, password, credit, pseudo, registrationDate, dateOfBirth, role, lastDateBonus) VALUES ('{obj.UserName}', '{obj.Password}', '10', '{obj.Pseudo}', '{formattedDateNow}', '{formattedDateOfBirth}', '0', '{ldb.ToString("yyyy-MM-dd")}')", connection);
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.[User] WHERE idUser = @id", connection);
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

        public override bool Update(Player obj)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.[User] SET credit = '{obj.Credit}', lastDateBonus = '{obj.LastDateBonus.ToString("yyyy-MM-dd")}' WHERE userName = @name", connection);
                cmd.Parameters.AddWithValue("name", obj.UserName);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }

        public override Player Find(int id)
        {

            Player player = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))

                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[User] WHERE idUser = @id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            player = new Player
                            {
                                IdPlayer = reader.GetInt32("idUser"),
                                Pseudo = reader.GetString("pseudo"),
                                UserName = reader.GetString("username"),
                                Password = reader.GetString("password"),
                                Credit = reader.GetInt32("credit"),
                                RegistrationDate = reader.GetDateTime("registrationDate"),
                                DateOfBirth = reader.GetDateTime("dateOfBirth"),
                                BookingsList = new List<Booking>(),
                                CopyList = new List<Copy>(),
                                LoanList = new List<Loan>(),
                            };
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return player;
        }

        public Player Login(string username, string password)
        {

            Player player = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))

                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[User] WHERE username = @username AND password = @password", connection);
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            player = new Player();

                            player.UserName = reader.GetString("username");
                            player.Password = reader.GetString("password");
                            player.RegistrationDate = reader.GetDateTime("registrationDate");
                            player.DateOfBirth = reader.GetDateTime("dateOfBirth");
                            player.Pseudo = reader.GetString("pseudo");
                            player.Credit = reader.GetInt32("credit");
                            player.Role = reader.GetBoolean("role");
                            player.LastDateBonus = reader.GetDateTime("lastDateBonus");

                        }
                    }
                }
            }


            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }

            return player;


        }
    

    public override List<Player> FindAll()
        {
            List<Player> players = new List<Player>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.VideoGame", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.RegistrationDate = reader.GetDateTime("registrationDate");
                        player.DateOfBirth = reader.GetDateTime("dateOfBirth");
                        player.Pseudo = reader.GetString("pseudo");
                        player.Credit = reader.GetInt32("credit");
                        player.Role = reader.GetBoolean("role");
                        players.Add(player);
                    }
                }
            }
            return players;

        }
    }
}
