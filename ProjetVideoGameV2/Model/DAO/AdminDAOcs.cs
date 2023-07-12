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
    internal class AdminDAO : DAO<Administrator>
    {
        public AdminDAO() { }

        public override bool Create(Administrator obj)
        {
            return false;
        }

        public override bool Update(Administrator obj)
        {
            return false;
        }
        public override Administrator Find(int id)
        {

            Administrator admin = null;
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
                            admin = new Administrator
                            {

                                UserName = reader.GetString("username"),
                                Password = reader.GetString("password"),


                            };
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return admin;
        }

        public Administrator Login(string username, string password)
        {

            Administrator admin = null;
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
                            admin = new Administrator();

                            admin.UserName = reader.GetString("username");
                            admin.Password = reader.GetString("password");
                            admin.Role = reader.GetBoolean("role");

                        }
                    }
                }
            }


            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }

            return admin;


        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Administrator> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
