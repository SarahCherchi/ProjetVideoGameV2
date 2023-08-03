using ProjetVideoGameV2;
using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.Model.DAO;
using ProjetVideoGameV2.POCO;
using ProjetVideoGameV2.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace ProjectVideoGameV2.View
{

    public partial class Home_Page : UserControl
    {

        private Player player;
        private VideoGames selectedVg;
        private List<Booking> bookings = new List<Booking>();
        private List<Booking> waitingList = new List<Booking>();  

        public Home_Page(Player player)
        {
            InitializeComponent();
            this.player = player;
            //waitingList.AddRange(Booking.findAllBookingByIdVideoGame(selectedVg.IdVideoGames));

            bool ok = player.addBirthdayBonus();
            if (ok)
            {
                Loaded += Home_Page_Loaded;
            }
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
            List<VideoGames> vg = VideoGames.FindAll();

            foreach (var game in vg)
            {
                game.NumberOfCopy = VideoGames.nbrCopyAvailable(game.IdVideoGames);
            }

            dgVideoGames.ItemsSource = vg;

            dgVideoGames.SelectionChanged += dgVideoGames_SelectionChanged;
          
        }

        private void Home_Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();

            ShowBirthdayMessage();
        }

        private void ShowBirthdayMessage()
        {
            if (player.bonusReceived)
            {
                MessageBox.Show("Congratulations! You have won 2 credits for your birthday!", "Birthday Bonus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void dgVideoGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVideoGames.SelectedItem is VideoGames selectedGame)
            {
                selectedVg = selectedGame;
            }
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            List<VideoGames> vg = VideoGames.FindVideoGamesByName(nameSearch.Text);
            dgVideoGames.ItemsSource = vg;
            foreach (var game in vg)
            {
                game.NumberOfCopy = VideoGames.nbrCopyAvailable(game.IdVideoGames);
            }
        }
        private void Button_Home(object sender, RoutedEventArgs e)
        {
            Home_Page home = new Home_Page(player);
            Content = home;
        }

        private void Button_Loan(object sender, RoutedEventArgs e)
        {
            Loan_Page loan = new Loan_Page(player);
            this.Content = loan;
        }

        private void Button_Copies(object sender, RoutedEventArgs e)
        {
            Copies_Page copies = new Copies_Page(player);
            this.Content = copies;
        }

        private void Button_Renting(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to add your copy?", "Add a copy", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Copy copy = new Copy();

                copy.VideoGames = selectedVg;
                copy.Owner = player;

                bool success = Copy.createCopy(copy);

                if (success)
                {
                    MessageBox.Show("Renting successful!", "Renting", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Renting failed!", "Renting", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            Home_Page home_Page = new Home_Page(player);
            Content = home_Page;
            
        }

        private void Button_Booking(object sender, RoutedEventArgs e)
        {
            if(selectedVg.NumberOfCopy > 0)
            {
                if (isEnoughCredit(player, selectedVg))
                {
                    Booking_Page book = new Booking_Page(selectedVg, player);
                    this.Content = book;
                }
                else
                {
                    MessageBox.Show("You don't have enough credits for book this game. Please lend one of your games first");
                }
            }
            else
            {
                if(isEnoughCredit(player, selectedVg)) 
                {
                    if (isAllowedToWaitingList(selectedVg))
                    {
                        MessageBoxResult result = MessageBox.Show($"Do you want to get on the waiting list for {selectedVg.Name} ?", "New Booking", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            createNewBooking(selectedVg);
                            MessageBox.Show($"You are placed on the waiting list and you are {waitingList.Count} people waiting");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot are placed on the waiting list a second time", "Error Booking", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("You don't have enough credit to be place to the waiting list. Please lend one of your games first");
                }
            }
        }

        private bool isEnoughCredit(Player player, VideoGames videoGames)
        {
            if (player.Credit > 0 && player.Credit >= videoGames.CreditCost)
            {
                return true;
            }
            return false;
        }

        public bool isAllowedToWaitingList(VideoGames videoGames)
        {
            foreach (var booking in waitingList)
            {
                if (booking.Player.IdPlayer == player.IdPlayer && booking.VideoGames.IdVideoGames == videoGames.IdVideoGames)
                {
                        return false;
                }
            } 
            return true;
        }


        private Player generatePlayerHaveCopy(int id)
        {
            bookings = Booking.findAllBookingByIdVideoGame(id);

            // Tri des réservations selon l'ordre de priorité
            SortBookingsByPriority();

            // Sélection du premier joueur avec suffisamment de crédits
            Player selectedPlayer = bookings.FirstOrDefault(booking => booking.Player.Credit > 0)?.Player;

            return selectedPlayer;
        }

        private void SortBookingsByPriority()
        {
            bookings.Sort((booking1, booking2) =>
            {
                // Trier par le plus grand nombre d'unités sur le compte (ordre décroissant)
                int creditComparison = booking2.Player.Credit.CompareTo(booking1.Player.Credit);
                if (creditComparison != 0)
                    return creditComparison;

                // Trier par la réservation la plus ancienne (ordre croissant)
                int bookingDateComparison = booking1.BookingDate.CompareTo(booking2.BookingDate);
                if (bookingDateComparison != 0)
                    return bookingDateComparison;

                // Trier par l'abonné inscrit depuis le plus longtemps (ordre décroissant)
                int registrationDateComparison = booking2.Player.RegistrationDate.CompareTo(booking1.Player.RegistrationDate);
                if (registrationDateComparison != 0)
                    return registrationDateComparison;

                // Trier par l'abonné le plus âgé (ordre décroissant)
                int ageComparison = booking2.Player.DateOfBirth.CompareTo(booking1.Player.DateOfBirth);
                if (ageComparison != 0)
                    return ageComparison;

                // Trier aléatoirement si tous les critères sont égaux
                return Guid.NewGuid().CompareTo(Guid.NewGuid());
            });
        }

        private void createNewBooking(VideoGames videoGames)
        {
            Booking booking = new Booking();
            booking.BookingDate = DateTime.Now;
            booking.VideoGames = videoGames;
            booking.Player = player;
            bool success = Booking.createBooking(booking);
            if (success)
            {
                waitingList.AddRange(Booking.findAllBookingByIdVideoGame(selectedVg.IdVideoGames));
            }
            else
            {
                MessageBox.Show("Error at the creation of the booking", "Error Booking", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Window home_page = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();

            home_page.Close();
            mainWindow.Show();

        }
    }
}
