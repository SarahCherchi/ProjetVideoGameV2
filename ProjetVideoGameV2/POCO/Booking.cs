using System;

namespace ProjetVideoGameV2.POCO
{
    internal class Booking
    {
        private int idBooking;
        private DateTime bookingDate;
        private VideoGames videoGames;
        private Player player;

        public Booking()
        {

        }

        public Booking(int idBooking, DateTime bookingDate)
        {
            this.idBooking = idBooking;
            this.bookingDate = bookingDate;
        }

        public Booking(int idBooking, DateTime bookingDate,VideoGames videoGames,Player player)
        {
            this.idBooking = idBooking;
            this.bookingDate = bookingDate;
            this.videoGames = videoGames;
            this.player = player;
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

    }
}
