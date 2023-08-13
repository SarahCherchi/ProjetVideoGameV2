using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProjetVideoGameV2.View
{
    public partial class BookingList_Page : UserControl
    {
        private Player player;
        private List<Booking> bookings = new List<Booking>();
        private ICollectionView collectionView;
        public BookingList_Page(Player player)
        {
            InitializeComponent();
            this.player = player;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
            initDgBooking();
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

        private void Button_BookingList(object sender, RoutedEventArgs e)
        {
            BookingList_Page bookingList = new BookingList_Page(player);
            this.Content = bookingList;
        }

        private void Button_Account(object sender, RoutedEventArgs e)
        {
            AccountInfo account = new AccountInfo(player);
            this.Content = account;
        }

        private void Button_History(object sender, RoutedEventArgs e)
        {
            History_Page history = new History_Page(player);
            this.Content = history;
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Window home_page = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();

            home_page.Close();
            mainWindow.Show();

        }

        private void Button_CancelBooking(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to withdraw from the waiting list ?", "Cancel Booking", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                Booking booking = (Booking) dgBookingList.SelectedItem;
                bool success = Booking.deleteBooking(booking.Idbooking);
                if(success)
                {
                    BookingList_Page bookingList = new BookingList_Page(player);
                    Content = bookingList;
                    MessageBox.Show("Booking cancelled", "Success", MessageBoxButton.OK ,MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("An error has occurred", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void initDgBooking()
        {
            bookings = Booking.findAllBooking();
            collectionView = CollectionViewSource.GetDefaultView(bookings);
            dgBookingList.ItemsSource = collectionView;

            bookings.RemoveAll(booking => booking.Player.IdPlayer != player.IdPlayer);

            foreach (var booking in bookings)
            {
                booking.VideoGames = VideoGames.FindVideoGames(booking.VideoGames.IdVideoGames);
                booking.Player = (Player)Player.findPlayer(booking.Player.IdPlayer);
                booking.NumberOfPlayers = Booking.CountNumberOfPlayerOnWaitingList(booking.VideoGames.IdVideoGames);
            }
        }
    }
}
