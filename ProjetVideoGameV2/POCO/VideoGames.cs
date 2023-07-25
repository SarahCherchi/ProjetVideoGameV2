using ProjetVideoGameV2.Model.Dao;
using System.Collections.Generic;

namespace ProjetVideoGameV2.POCO

{
    public class VideoGames
    {
        private int idVideoGames;
        private string name;
        private int creditCost;
        private string console;
        private int numberOfCopy;
        private List<Booking> bookingList;
        private List<Copy> copyList;
        private static VideoGamesDAO videoGamesDAO = new VideoGamesDAO(); 

        

        public VideoGames()
        {

        }

        public VideoGames(int idVideoGames, string name, int creditCost, string console, List<Booking> bookingList, List<Copy> copyList)
        {
            this.idVideoGames = idVideoGames;
            this.name = name;
            this.creditCost = creditCost;
            this.console = console;
            this.bookingList = bookingList;
            this.copyList = copyList;
        }

        public int IdVideoGames
        {
            get { return idVideoGames; }
            set { idVideoGames = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int CreditCost
        {
            get { return creditCost; }
            set { creditCost = value; }
        }

        public string Console
        {
            get { return console; }
            set { console = value; }
        }

        public int NumberOfCopy
        {
            get { return numberOfCopy; }
            set { numberOfCopy = value; }
        }

        public List<Booking> BookingList
        {
            get { return bookingList; }
            set { bookingList = value; }
        }

        public List<Copy> CopyList
        {
            get { return copyList; }
            set { copyList = value; }
        }

        public static bool CreateVideoGame(VideoGames vg)
        {
            return videoGamesDAO.Create(vg);
        }

        public static bool UpdateCreditCost(VideoGames vg)
        {
            return videoGamesDAO.Update(vg);
        }

        public static VideoGames FindVideoGames(int id) 
        {
            return videoGamesDAO.Find(id);
        }

        public static List<VideoGames> FindAll()
        {
            return videoGamesDAO.FindAll();
        }

        public static bool FindVgByNameConsole(VideoGames vg)
        {
            return videoGamesDAO.FindDuplcateVg(vg);
        }

        public static int nbrCopyAvailable(int id)
        {
            return videoGamesDAO.nbrCopyAvailable(id);
        }

        public static List<Copy> CopyAvailable(int id)
        {
            return videoGamesDAO.CopyAvailable(id);
        }

    }
}
