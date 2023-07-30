using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ProjetVideoGameV2.Model.DAO
{
    internal class CopyDAO : DAO<Copy>
    {
        public CopyDAO() { }

        public override bool Create(Copy obj)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Copy(idVideoGame,owner) VALUES('{obj.VideoGames.IdVideoGames}','{obj.Owner.IdPlayer}')", connection);
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Copy WHERE idCopy = @id", connection);
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

        public override bool Update(Copy obj)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Copy SET idLoan = '{obj.Loan.IdLoan}' WHERE idCopy = @id", connection);
                cmd.Parameters.AddWithValue("id", obj.IdCopy);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }
        public override Copy Find(int id)
        {

            Copy copy = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))

                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE idCopy = @id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            copy = new Copy();
                            {
                                copy.IdCopy = Convert.ToInt32(reader.GetString("idCopy"));
                                copy.VideoGames.IdVideoGames = Convert.ToInt32("idVideoGame");
                                copy.Owner.IdPlayer = Convert.ToInt32("owner");
                                copy.Loan.IdLoan = Convert.ToInt32("idLoan");
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return copy;
        }


        public override List<Copy> FindAll()
        {
            List<Copy> copies = new List<Copy>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy copy = new Copy();
                        copy.IdCopy = reader.GetInt32("idCopy");
                        copy.VideoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                        copy.Owner.IdPlayer = reader.GetInt32("owner");
                        copy.Loan.IdLoan = reader.GetInt32("idLoan");
                        copies.Add(copy);
                        
                    }
                }
            }
            return copies;

        }

        public List<Copy> FindAllCopyVideoGame(int id)
        {
            List<Copy> copies = new List<Copy>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE idVideoGame = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy copy = new Copy();
                        copy.IdCopy = reader.GetInt32("idCopy");
                        copy.Owner = new Player();
                        copy.Owner.IdPlayer = reader.GetInt32("owner");
                        
                        copies.Add(copy);

                    }
                }
            }
            return copies;

        }

        public List<Copy> FindAllCopyOwner(int id)
        {
            List<Copy> copies = new List<Copy>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE owner = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy copy = new Copy();
                        copy.IdCopy = reader.GetInt32("idCopy");
                        copy.VideoGames = new VideoGames();
                        copy.VideoGames.IdVideoGames = reader.GetInt32("idVideoGame");

                        copies.Add(copy);

                    }
                }
            }
            return copies;
        }

        public bool IsAvailable(int id)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Copy WHERE idCopy = @id AND idLoan IS NULL", connection);
                cmd.Parameters.AddWithValue("id", id);
                connection.Open();
                int count = (int)cmd.ExecuteScalar();
                success = count > 0;
            }

            return success;
        }

        public bool UpdateIdLoanCopy(Copy obj)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Copy SET idLoan = NULL WHERE idCopy = @id", connection);
                cmd.Parameters.AddWithValue("id", obj.IdCopy);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }

    }
}

