using ProjetVideoGameV2.Model.Dao;
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
        private static UserDAO userDAO = new UserDAO();


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

        public string StartDateString
        {
            get { return startDate.ToString("dd-MM-yyyy"); }

        }

        public string EndDateString 
        {
            get { return endDate.ToString("dd-MM-yyyy"); }
                
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

        public string BorrowerUsername
        {
            get; set;
        }

        public string LenderUsername
        {
             get; set; 
        }

        public string BorrowerPseudo
        {
            get { return Borrower.Pseudo; }
        }

        public string LenderPseudo
        {
            get { return Lender.Pseudo; }
        }

        public string VideoGameName
        {
            get { return Copy.VideoGames.Name; }
        }

        public int IdCopy
        {
            get { return Copy.IdCopy; }
        }

        public static int createLoan(Loan loan)
        {
            return loanDAO.CreateLoan(loan);
        }

        public static bool deleteLoan(int id)
        {
            return loanDAO.Delete(id);
        }

        public static bool EndLoan(Loan loan)
        {
            loan.ongoing = false;
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

        public static List<Loan> findAllLoanByIdLender(int id, int idc)
        {
            return loanDAO.FindAllByLender(id, idc);
        }

        public static List<Loan> findAllLoanByIdBorrower(int id)
        {
            return loanDAO.FindAllByBorrower(id);
        }

        public static int calculateBalance(Loan l,Player pBorrower)
        {
            int balance = 0;
            
            if(l.EndDate < DateTime.Now)
            {
                TimeSpan difference = DateTime.Now - l.EndDate;
                int nbrDaysOverdue = (int)difference.TotalDays;
                balance = 5 * nbrDaysOverdue;
                pBorrower.Credit -= balance;
                l.Lender.Credit += balance;
                userDAO.UpdateCreditBalancePenalty(pBorrower,l.Lender);
                return balance;
            }
            return balance;
        }
    }
}
