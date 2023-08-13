using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace ProjetVideoGameV2.View
{
    public partial class Copies_Page : UserControl
    {
        private Player player;
        private Copy selectedCopy;

        public Copies_Page(Player player)
        {
            InitializeComponent();
            this.player = player;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
            initDgCopy();
        }

        private void Button_ViewMore(object sender, RoutedEventArgs e)
        {
            BorrowHistory userCopiesBook = new BorrowHistory(player,selectedCopy);
            this.Content = userCopiesBook;
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

        private void Button_Account(object sender, RoutedEventArgs e)
        {
            AccountInfo account = new AccountInfo(player);
            this.Content = account;
        }

        private void Button_BookingList(object sender, RoutedEventArgs e)
        {
            BookingList_Page bookingList = new BookingList_Page(player);
            this.Content = bookingList;
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

        private void initDgCopy()
        {
            List<Copy> cp = Copy.findAllCopiesByUser(player.IdPlayer);
            dgCopies.ItemsSource = cp;

            foreach (Copy copy in cp)
            {
                copy.VideoGames = VideoGames.FindVideoGames(copy.VideoGames.IdVideoGames);
                copy.Available = Copy.IsAvailable(copy.IdCopy);
            }
        }

        private void dgCopies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgCopies.SelectedItem is Copy myCopy)
            {
                selectedCopy = myCopy;
            }
        }
    }
}
