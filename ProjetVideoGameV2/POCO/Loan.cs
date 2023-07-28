using ProjetVideoGameV2.Model.DAO;
using System;
using System.Collections.Generic;

namespace ProjetVideoGameV2.POCO
{
    public class Loan
    {
        private int idLoan;
        private DateTime startDate;
        private DateTime endDate;
        private bool ongoing;
        private Copy copy;
        private Player lender;
        private Player borrower;
        private static LoanDAO loanDAO = new LoanDAO();


        public Loan()
        {

        }

        public Loan(int idLoan, DateTime startDate, DateTime endDate, bool ongoing)
        {
            this.idLoan = idLoan;
            this.startDate = startDate;
            this.endDate = endDate;
            this.ongoing = ongoing;
        }

        public Loan(int idLoan, DateTime startDate, DateTime endDate, bool ongoing, Copy copy, Player lender, Player borrower)
        {
            this.idLoan = idLoan;
            this.startDate = startDate;
            this.endDate = endDate;
            this.ongoing = ongoing;
            this.copy = copy;
            this.lender = lender;
            this.borrower = borrower;
            
        }

        public int IdLoan
        {
            get { return idLoan; }
            set { idLoan = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public bool Ongoing
        {
            get { return ongoing; }
            set { ongoing = value; }
        }

        public Copy Copy
        {
            get { return copy; }
            set { copy = value; }
        }

        public Player Lender
        {
            get { return lender; }
            set{ lender = value;}
        }

        public Player Borrower
        {
            get { return borrower; }
            set { borrower = value; }
        }

        public static int createLoan(Loan loan)
        {
            return loanDAO.CreateLoan(loan);
        }

        public static bool deleteLoan(int id)
        {
            return loanDAO.Delete(id);
        }

        public static bool updateLoan(Loan loan)
        {
            return loanDAO.Update(loan);
        }

        public static Loan findLoan(int id)
        {
            return loanDAO.Find(id);
        }

        public static List<Loan> findAllLoan()
        {
            return loanDAO.FindAll();
        }

        public static List<Loan> findAllLoanByIdPlayer(int id)
        {
            return loanDAO.FindAllByIdPlayer(id);
        }
    }
}
