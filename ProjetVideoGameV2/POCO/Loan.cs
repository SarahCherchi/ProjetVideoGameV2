using System;

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
    }
}
