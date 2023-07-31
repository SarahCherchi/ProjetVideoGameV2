using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.Model.DAO;
using System;
using System.Collections.Generic;

namespace ProjetVideoGameV2.POCO

{
    public class Player : User
    {
        private int idPlayer;
        private int credit;
        private string pseudo;
        private DateTime registrationDate;
        private DateTime dateOfBirth;
        private DateTime lastDateBonus;
        private List<Booking> bookingsList;
        private List<Copy> copyList;
        private List<Loan> loanList;
        private static UserDAO userDAO = new UserDAO();

        public bool bonusReceived;

        public Player()
        {

        }

        public Player(int idPlayer, int credit, string pseudo, DateTime registrationDate, DateTime dateOfBirth)
        {
            this.idPlayer = idPlayer;
            this.credit = credit;
            this.pseudo = pseudo;
            this.registrationDate = registrationDate;
            this.dateOfBirth = dateOfBirth;
        }


        public int IdPlayer
        {
            get { return idPlayer; }
            set { idPlayer = value; }
        }

        public string RegistrationDateString
        {
            get { return registrationDate.ToString("dd-MM-yyyy"); }

        }

        public string DateOfBirthString
        {
            get { return registrationDate.ToString("dd-MM-yyyy"); }

        }

        public string UsernameString
        {
            get { return UserName.ToString(); }
        }

        public int Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        public string Pseudo
        {
            get { return pseudo; }
            set { pseudo = value; }
        }

        public DateTime RegistrationDate
        {
            get { return registrationDate; }
            set { registrationDate = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public DateTime LastDateBonus
        {
            get { return lastDateBonus; }
            set { lastDateBonus = value; }
        }

        public List<Booking> BookingsList
        {
            get { return bookingsList; }
            set { bookingsList = value; }
        }

        public List<Loan> LoanList
        {
            get { return loanList; }
            set { loanList = value; }
        }

        public List<Copy> CopyList
        {
            get { return copyList; }
            set { copyList = value; }
        }

        public static bool createPlayer(Player player)
        {
            return userDAO.Create(player);
        }

        public static bool deletePlayer(int id)
        {
            return userDAO.Delete(id);
        }

        public static bool updatePlayer(Player player)
        {
            return userDAO.Update(player);
        }

        public static User findPlayer(int id)
        {
            return userDAO.Find(id);
        }


        public static List<User> findAllPlayer()
        {
            return userDAO.FindAll();
        }

        public void addBirthdayBonus()
        {

            DateTime now = DateTime.Now.Date;

            if (DateOfBirth.Day == DateTime.Now.Day && DateOfBirth.Month == DateTime.Now.Month && LastDateBonus.AddYears(1) <= now)
            {
                Credit += 2;
                lastDateBonus = now;
                bonusReceived = userDAO.Update(this);
            }
        }

        public static Player findPlayerByUsername(string username)
        {
            return userDAO.FindUserName(username);
        }
    }
}