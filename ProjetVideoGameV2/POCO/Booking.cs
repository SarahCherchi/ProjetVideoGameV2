using ProjetVideoGameV2.Model.DAO;
using System;
using System.Collections.Generic;

namespace ProjetVideoGameV2.POCO
{
    public class Booking
    {
        private int idBooking;
        private DateTime bookingDate;
        private VideoGames videoGames;
        private Player player;
        private static BookingDAO bookingDAO = new BookingDAO();

        public Booking()
        {

        }

        public int Idbooking 
        {
            get { return idBooking; }
            set { idBooking = value; }
        }
        public DateTime BookingDate
        {
            get { return bookingDate; }
            set { bookingDate = value; }
        }

        public VideoGames VideoGames
        {
            get { return videoGames; }
            set { videoGames = value; }
        }

        public Player Player
        { 
            get { return player; }
            set { player = value; }
        }

        public string BookingDateString
        {
            get { return BookingDate.ToString("dd-MM-yyyy"); }
        }

        public string VideoGamesString
        {
            get { return VideoGames.Name; }
        }

        public string PseudoString
        {
            get { return Player.Pseudo; }
        }

        public int NumberOfPlayers
        {
            get; set;
        }

        public static bool createBooking(Booking booking)
        {
            return bookingDAO.Create(booking);
        }

        public static bool deleteBooking(int id)
        {
            return bookingDAO.Delete(id);
        }

        public static bool deleteBookingByIdUserAndIdVideoGame(int idUser, int idVideoGame)
        {
            return bookingDAO.DeleteByIdUserAndIdVideoGames(idUser, idVideoGame);
        }

        public static bool updateBooking(Booking booking)
        {
            return bookingDAO.Update(booking);
        }

        public static Booking findBooking(int id)
        {
            return bookingDAO.Find(id);
        }

        public static List<Booking> findAllBooking()
        {
            return bookingDAO.FindAll();
        }

        public static List<Booking> findAllBookingByIdVideoGame(int id)
        {
            return bookingDAO.FindAllByIdVidegoGame(id);
        }

        public static int CountNumberOfPlayerOnWaitingList(int id)
        {
            return bookingDAO.CountMemberOnWaitingList(id);
        }
    }
}
