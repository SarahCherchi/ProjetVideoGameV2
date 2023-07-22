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
                        loan.Ongoing = reader.GetBoolean("ongoing");
                        loan.Copy.IdCopy = reader.GetInt32("idCopy");
                        loan.Lender.IdPlayer = reader.GetInt32("lender");
                        loan.Borrower.IdPlayer = reader.GetInt32("borrower");
                        loans.Add(loan);
                    }
                }
            }
            return loans;

        }
    }
}

