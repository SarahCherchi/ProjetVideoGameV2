using ProjetVideoGameV2.Model.Dao;
using System.Collections.Generic;

namespace ProjectVideoGameV2.POCO

{
    internal class VideoGames
    {
        private int idVideoGames;
        private string name;
        private int creditCost;
        private string console;
        private List<Booking> bookingList;
        private List<Copy> copyList;

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


        public static List<VideoGames> FindAll()
        {
            VideoGamesDAO vgDAO = new VideoGamesDAO();
            return vgDAO.FindAll();
        }

        public bool CopyAvailable(int id)
        {
            VideoGamesDAO vgDAO = new VideoGamesDAO();
            return vgDAO.CopyAvailable(id);
        }
    }
}
