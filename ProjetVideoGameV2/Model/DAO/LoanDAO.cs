﻿using ProjetVideoGameV2.POCO;
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
    internal class LoanDAO : DAO<Loan>
    {
        public LoanDAO() { }
        
            public override bool Create(Loan obj)
               {
                   bool success = false;
                   string formattedEndDate = obj.EndDate.ToString("yyyy-MM-dd");
                   string formattedDateNow = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                   using (SqlConnection connection = new SqlConnection(this.connectionString))
                   {
                       SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Loan (startDate, endDate, ongoing, idCopy, lender, borrower) VALUES ('{formattedDateNow}', '{formattedEndDate}', '{obj.Ongoing}', '{obj.Copy.IdCopy}', '{obj.Lender.IdPlayer}', '{obj.Borrower.IdPlayer}')", connection);
                       connection.Open();
                       int res = cmd.ExecuteNonQuery();
                       success = res > 0;
                   }

                   return success;
               }
        
            public int CreateLoan(Loan obj)
            {
                int idLoan;
                string formattedEndDate = obj.EndDate.ToString("yyyy-MM-dd");
                string formattedDateNow = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Loan (startDate, endDate, ongoing, idCopy, lender, borrower) VALUES ('{formattedDateNow}', '{formattedEndDate}', '{obj.Ongoing}', '{obj.Copy.IdCopy}', '{obj.Lender.IdPlayer}', '{obj.Borrower.IdPlayer}')", connection, transaction);
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("SELECT SCOPE_IDENTITY();", connection, transaction);
                            idLoan = Convert.ToInt32(cmd.ExecuteScalar());

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                return idLoan;
            }

        public override bool Delete(int id)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Loan WHERE idLoan = @id", connection);
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

        public override bool Update(Loan obj)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Loan SET ongoin = '{obj.Ongoing}' WHERE idLoan = @idLoan", connection);
                cmd.Parameters.AddWithValue("idLoan", obj.IdLoan);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }

            return success;
        }

        public override Loan Find(int id)
        {

            Loan loan = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))

                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Loan WHERE idLoan = @idLoan", connection);
                    cmd.Parameters.AddWithValue("idLoan", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            loan = new Loan();
                            {
                                loan.IdLoan = reader.GetInt32("idLoan");
                                loan.StartDate = reader.GetDateTime("startDate");
                                loan.EndDate = reader.GetDateTime("endDate");
                                loan.Ongoing = reader.GetBoolean("ongoing");
                                loan.Copy.IdCopy = reader.GetInt32("idCopy");
                                loan.Lender.IdPlayer = reader.GetInt32("lender");
                                loan.Borrower.IdPlayer = reader.GetInt32("borrower");
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Une erreur sql s'est produite!");
            }
            return loan;
        }

        public override List<Loan> FindAll()
        {
            List<Loan> loans = new List<Loan>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Loan", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan loan = new Loan();
                        loan.IdLoan = reader.GetInt32("idLoan");
                        loan.StartDate = reader.GetDateTime("startDate");
                        loan.EndDate = reader.GetDateTime("endDate");
                        Copy copy = new Copy();
                        copy.IdCopy = reader.GetInt32("idCopy");
                        loan.Copy = copy;
                        Player player = new Player();
                        player.IdPlayer = reader.GetInt32("lender");
                        loan.Lender = player;
                        Player borrower = new Player();
                        player.IdPlayer = reader.GetInt32("borrower");
                        loan.Borrower = player;
                        loans.Add(loan);
                    }
                }
            }
            return loans;
        }

        public List<Loan> FindAllByLender(int id, int idc)
        {
            List<Loan> loans = new List<Loan>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Loan WHERE lender = @idLoan AND idCopy = @idCopy", connection);
                cmd.Parameters.AddWithValue("idLoan", id);
                cmd.Parameters.AddWithValue("idCopy", idc);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan loan = new Loan();
                        loan.IdLoan = reader.GetInt32("idLoan");

                        loan.StartDate = reader.GetDateTime("startDate");
                        loan.EndDate = reader.GetDateTime("endDate");

                        loan.Ongoing = reader.GetBoolean("ongoing");
                        Copy copy = new Copy();
                        copy.IdCopy = reader.GetInt32("idCopy");
                        loan.Copy = copy;
                        Player borrower = new Player();
                        borrower.IdPlayer = reader.GetInt32("borrower");
                        loan.Borrower = borrower;
                        loans.Add(loan);
                    }
                }
            }
            return loans;
        }

        public List<Loan> FindAllByBorrower(int id)
        {
            List<Loan> loans = new List<Loan>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Loan WHERE borrower = @idLoan AND ongoing = 'True'", connection);
                cmd.Parameters.AddWithValue("idLoan", id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan loan = new Loan();
                        loan.IdLoan = reader.GetInt32("idLoan");

                        loan.StartDate = reader.GetDateTime("startDate");
                        loan.EndDate = reader.GetDateTime("endDate");

                        loan.Ongoing = reader.GetBoolean("ongoing");
                        Copy copy = new Copy();
                        copy.IdCopy = reader.GetInt32("idCopy");
                        loan.Copy = copy;
                        Player lender = new Player();
                        lender.IdPlayer = reader.GetInt32("lender");
                        loan.Lender = lender;
                        loans.Add(loan);
                    }
                }
            }
            return loans;
        }

    }
}

