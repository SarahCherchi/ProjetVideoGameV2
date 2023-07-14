using ProjetVideoGameV2.Model.Dao;
using System;
using System.Collections.Generic;

namespace ProjetVideoGameV2.POCO

{
    internal class Player : User
    {
        private int idPlayer;
        private int credit;
        private string pseudo;
        private DateTime registrationDate;
        private DateTime dateOfBirth;
        private List<Booking> bookingsList;
        private List<Copy> copyList;
        private List<Loan> loanList;
        private static PlayerDAO playerDAO = new PlayerDAO();

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

        public List<Booking> BookingsList
        {
            get { return bookingsList; }
            set { bookingsList = value; }
        }

        public List <Loan> LoanList
        {
            get { return loanList; }
            set { loanList = value; }
        }

        public List<Copy> CopyList
        {
            get { return copyList; }
            set{copyList = value; }
        }

        public static bool createPlayer(Player player)
        {
            return playerDAO.Create(player);
        }

        public static bool deletePlayer(int id)
        {
            return playerDAO.Delete(id);
        }

        public static bool updatePlayer(Player player)
        {
            return playerDAO.Update(player);
        }

        public static Player findPlayer(int id)
        {
            return playerDAO.Find(id);
        }

        public static Player loginPlayer(String username, String pw)
        {
            return playerDAO.Login(username, pw);
        }

        public static List<Player> findAllPlayer()
        {
            return playerDAO.FindAll();
        }

        public void addBirthday()
        {
            Player player = findPlayer(idPlayer);
            if (player.DateOfBirth.Day == DateTime.Now.Day && player.DateOfBirth.Month == DateTime.Now.Month)
            {
                player.Credit += 2;
            }
        }
    }
}