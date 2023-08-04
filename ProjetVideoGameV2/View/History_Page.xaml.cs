using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetVideoGameV2.View
{
    public partial class History_Page : UserControl
    {
        private Player player;
        private List<Loan> loans = new List<Loan>();
        private ICollectionView collectionView;
        public History_Page(Player player)
        {
            InitializeComponent();
            this.player = player;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;

            loans = Loan.findAllLoanHistory(player.IdPlayer);
            collectionView = CollectionViewSource.GetDefaultView(loans);
            dgLoanHistory.ItemsSource = collectionView;

            foreach (var loan in loans)
            {
                loan.Lender = (Player)Player.findPlayer(loan.Lender.IdPlayer);
                loan.Copy = Copy.findCopy(loan.Copy.IdCopy);
                loan.Copy.VideoGames = VideoGames.FindVideoGames(loan.Copy.VideoGames.IdVideoGames);
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
    }
}
