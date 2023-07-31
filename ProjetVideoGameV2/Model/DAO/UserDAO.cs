using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVideoGameV2.Model.DAO
{
    internal class UserDAO : DAO<User>
    {
        public UserDAO() { }

        public override bool Create(User obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update(User obj)
        {
            throw new NotImplementedException();
        }

        public bool Create(Player obj)
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

        public bool Update(Player obj)
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
            }
            return success;
        }

        public override User Find(int id)
        {
            User user = null;
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
                            if (reader.GetBoolean("role")) // Check if the user is an administrator
                            {
                                user = new Administrator
                                {
                                    UserName = reader.GetString("username"),
                                    Password = reader.GetString("password"),
                                    Role = true
                                };
                            }
                            else
                            {
                                user = new Player
                                {
                                    IdPlayer = reader.GetInt32("idUser"),
                                    Pseudo = reader.GetString("pseudo"),
                                    UserName = reader.GetString("username"),
                                    Password = reader.GetString("password"),
                                    Credit = reader.GetInt32("credit"),
                                    RegistrationDate = reader.GetDateTime("registrationDate"),
                                    DateOfBirth = reader.GetDateTime("dateOfBirth"),
                                    Role = false,
                                    LastDateBonus = reader.GetDateTime("lastDateBonus"),
                                    BookingsList = new List<Booking>(),
                                    CopyList = new List<Copy>(),
                                    LoanList = new List<Loan>()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return user;
        }

        public User Login(string username, string password)
        {
            User user = null;
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
                            if (reader.GetBoolean("role")) // Check if the user is an administrator
                            {
                                user = new Administrator
                                {
                                    UserName = reader.GetString("username"),
                                    Password = reader.GetString("password"),
                                    Role = true
                                };
                            }
                            else
                            {
                                user = new Player
                                {
                                    IdPlayer = reader.GetInt32("idUser"),
                                    UserName = reader.GetString("username"),
                                    Password = reader.GetString("password"),
                                    RegistrationDate = reader.GetDateTime("registrationDate"),
                                    DateOfBirth = reader.GetDateTime("dateOfBirth"),
                                    Pseudo = reader.GetString("pseudo"),
                                    Credit = reader.GetInt32("credit"),
                                    Role = false,
                                    LastDateBonus = reader.GetDateTime("lastDateBonus")
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }

            return user;
        }

        public override List<User> FindAll()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[User]", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetBoolean("role")) // Check if the user is an administrator
                        {
                            users.Add(new Administrator
                            {
                                UserName = reader.GetString("username"),
                                Password = reader.GetString("password"),
                                Role = true
                            });
                        }
                        else
                        {
                            users.Add(new Player
                            {
                                IdPlayer = reader.GetInt32("idUser"),
                                Pseudo = reader.GetString("pseudo"),
                                UserName = reader.GetString("username"),
                                Password = reader.GetString("password"),
                                Credit = reader.GetInt32("credit"),
                                RegistrationDate = reader.GetDateTime("registrationDate"),
                                DateOfBirth = reader.GetDateTime("dateOfBirth"),
                                Role = false,
                                LastDateBonus = reader.GetDateTime("lastDateBonus"),
                                BookingsList = new List<Booking>(),
                                CopyList = new List<Copy>(),
                                LoanList = new List<Loan>()
                            });
                        }
                    }
                }
            }
            return users;
        }

        public bool UpdateCreditBalancePenalty(Player obj1, Player obj2)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd1 = new SqlCommand($"UPDATE dbo.[User] SET credit = '{obj1.Credit}' WHERE idUser = @id", connection);
                SqlCommand cmd2 = new SqlCommand($"UPDATE dbo.[User] SET credit = '{obj2.Credit}' WHERE idUser = @id", connection);
                cmd1.Parameters.AddWithValue("id", obj1.IdPlayer);
                cmd2.Parameters.AddWithValue("id", obj2.IdPlayer);
                connection.Open();
                int res1 = cmd1.ExecuteNonQuery();
                int res2 = cmd2.ExecuteNonQuery();
                success = res1 > 0 && res2 > 0;
            }

            return success;
        }

        public Player FindUserName(string username)
        {
            Player player = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[User] WHERE username = @username", connection);
                    cmd.Parameters.AddWithValue("username", username);
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
                                Role = false
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

    }
}

