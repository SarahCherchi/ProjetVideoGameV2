
using ProjetVideoGameV2.Model.Dao;
using System.Collections.Generic;
using System;
using ProjetVideoGameV2.Model.DAO;

namespace ProjetVideoGameV2.POCO
{
    public class Copy
    {
        private int idCopy;
        private VideoGames videoGames;
        private Player owner;
        private Loan? loan;
        private bool available;
        private static CopyDAO copyDAO = new CopyDAO();

        public Copy()
        {

        }

        public Copy(int idCopy, VideoGames videoGames, Player owner, Loan loan)
        {
            this.idCopy = idCopy;
            this.videoGames = videoGames;
            this.owner = owner;
            this.loan = loan;
        }

        public string OwnerPseudo 
        { 
            get { return Owner.Pseudo; } 
        }

        public string VideoGameName
        {
            get { return VideoGames.Name; }
        }

        public string Console 
        { 
            get { return VideoGames.Console; } 
        }

        public int IdCopy
        {
            get { return idCopy; }
            set { idCopy = value; }
        }
        public VideoGames VideoGames
        {
            get { return videoGames; }
            set { videoGames = value; }
        }
        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public Loan Loan
        {
            get { return loan; }
            set { loan = value; }
        }
        public bool Available
        {
            get { return available; }
            set { available = value; }
        }

        public static bool createCopy(Copy copy)
        {
            return copyDAO.Create(copy);
        }

        public static bool deleteCopy(int id)
        {
            return copyDAO.Delete(id);
        }

        public static bool updateLoanerCopy(Copy copy)
        {
            return copyDAO.Update(copy);
        }

        public static bool ReleaseCopy(Copy copy)
        {
            return copyDAO.UpdateIdLoanCopy(copy);
        }

        public static Copy findCopy(int id)
        {
            return copyDAO.Find(id);
        }

        public static List<Copy> findAllCopies()
        {
            return copyDAO.FindAll();
        }

        public static bool IsAvailable(int id)
        {
           return copyDAO.IsAvailable(id);

        }

        public static List<Copy> findAllCopyByIdVG(int id)
        {
            return copyDAO.FindAllCopyVideoGame(id);
        }

        public static List<Copy> findAllCopiesByUser(int id)
        {
            return copyDAO.FindAllCopyOwner(id);
        }
    }
}
