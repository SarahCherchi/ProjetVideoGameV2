using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                                copy.IdCopy = reader.GetInt32("idCopy");
                                VideoGames videoGames = new VideoGames();
                                videoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                                copy.VideoGames = videoGames;
                                Player owner = new Player();
                                owner.IdPlayer = reader.GetInt32("owner");
                                copy.Owner = owner;
                                if (!reader.IsDBNull(reader.GetOrdinal("idLoan")))
                                {
                                    Loan loan = new Loan();
                                    loan.IdLoan = reader.GetInt32(reader.GetOrdinal("idLoan"));
                                    copy.Loan = loan;
                                }   
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
                        VideoGames videoGames = new VideoGames();
                        videoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                        copy.VideoGames = videoGames;
                        Player owner = new Player();
                        owner.IdPlayer = reader.GetInt32("owner");
                        copy.Owner = owner;
                        Loan loan = new Loan();
                        loan.IdLoan = reader.GetInt32("idLoan");
                        copy.Loan = loan;
                        copies.Add(copy);
                        
                    }
                }
            }
            return copies;
        }

        public List<Copy> FindAllCopyByVideoGame(int id)
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
                        Player owner = new Player();
                        owner.IdPlayer = reader.GetInt32("owner");
                        copy.Owner = owner;
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
                        VideoGames videoGames = new VideoGames();
                        videoGames.IdVideoGames = reader.GetInt32("idVideoGame");
                        copy.VideoGames = videoGames;
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

