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
        public bool bonusReceived;
        private int numberOfWeeks;
        private int totalCost;
        private static UserDAO userDAO = new UserDAO();

        public Player()
        {

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

        public DateTime LastDateBonus
        {
            get { return lastDateBonus; }
            set { lastDateBonus = value; }
        }

        public int NumberOfWeeks
        {
            get { return numberOfWeeks; }
            set { numberOfWeeks = value; }
        }

        public int TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        public string RegistrationDateString
        {
            get { return registrationDate.ToString("dd-MM-yyyy"); }

        }

        public string DateOfBirthString
        {
            get { return dateOfBirth.ToString("dd-MM-yyyy"); }

        }

        public string UsernameString
        {
            get { return UserName.ToString(); }
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

        public bool addBirthdayBonus()
        {

            DateTime now = DateTime.Now.Date;

            if (DateOfBirth.Day == DateTime.Now.Day && DateOfBirth.Month == DateTime.Now.Month && LastDateBonus.AddYears(1) <= now)
            {
                Credit += 2;
                lastDateBonus = now;
                bonusReceived = userDAO.Update(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Player findPlayerByUsername(string username)
        {
            return userDAO.FindUserName(username);
        }
    }
}