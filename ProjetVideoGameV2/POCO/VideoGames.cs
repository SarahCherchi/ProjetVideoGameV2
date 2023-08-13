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
        private static VideoGamesDAO videoGamesDAO = new VideoGamesDAO(); 

        

        public VideoGames()
        {

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

        public static List<VideoGames> FindVideoGamesByName(string nameVG)
        {
            return videoGamesDAO.GetVideoGamesByName(nameVG);
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
