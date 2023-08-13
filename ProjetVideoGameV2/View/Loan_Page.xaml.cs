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
    public partial class Loan_Page : UserControl
    {
        private Player player;
        private Player waitingPlayer;
        private List<Booking> waitingList;
        private List<Loan> loans;
        private ICollectionView collectionView;
        private Loan selectedLoan;
        public Loan_Page(Player player)
        {
            InitializeComponent();
            this.player = player;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
            refreshData();

        }

        private void refreshData()
        {
            lb_credit.Content = player.Credit;
            loans = Loan.findAllLoanByIdBorrower(player.IdPlayer);
            collectionView = CollectionViewSource.GetDefaultView(loans);
            dgLoan.ItemsSource = collectionView;

            foreach (var loan in loans)
            {
                loan.Lender = (Player) Player.findPlayer(loan.Lender.IdPlayer);
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
        private void Button_ReturnedCopy(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to return the copy?", "Confirm Return", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                Loan.EndLoan(selectedLoan);
                Copy.ReleaseCopy(selectedLoan.Copy);
                int latePenalty = Loan.calculateBalance(selectedLoan,player);

                MessageBox.Show("The copy has been returned successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                if(latePenalty > 0)
                {
                    MessageBox.Show($"You have returned your copy late. Your penalty is {latePenalty} credits.", "Overdue penalty",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }

                selectedLoan.Copy.VideoGames.NumberOfCopy = VideoGames.nbrCopyAvailable(selectedLoan.Copy.VideoGames.IdVideoGames);
                waitingList = Booking.findAllBookingByIdVideoGame(selectedLoan.Copy.VideoGames.IdVideoGames);

                if (selectedLoan.Copy.VideoGames.NumberOfCopy == 1 && waitingList.Count > 0)
                {
                    selectedLoan.Copy.IdCopy = Copy.findAllCopyByIdVG(selectedLoan.Copy.VideoGames.IdVideoGames).Last().IdCopy;
                    AllocateCopyToWaitingPlayer(selectedLoan.Copy.VideoGames, selectedLoan.Copy);
                    Booking.deleteBookingByIdUserAndIdVideoGame(waitingPlayer.IdPlayer, selectedLoan.Copy.VideoGames.IdVideoGames);
                    Player playerOwner = (Player) Player.findPlayer(selectedLoan.Copy.Owner.IdPlayer);
                    playerOwner.Credit = playerOwner.Credit + waitingPlayer.TotalCost;
                    Player.updatePlayer(playerOwner);
                    waitingPlayer.Credit = waitingPlayer.Credit - waitingPlayer.TotalCost;
                    Player.updatePlayer(waitingPlayer);
                }
                Loan_Page loanPage = new Loan_Page(player);
                Content = loanPage;
                refreshData();
            }
        }

        private void AllocateCopyToWaitingPlayer(VideoGames VideoGame, Copy copy)
        {
            waitingPlayer = generatePlayerHaveCopy(VideoGame.IdVideoGames);
            foreach (var player in ((App)Application.Current).PlayerList)
            {
                if (player.IdPlayer == waitingPlayer.IdPlayer)
                {
                    waitingPlayer.NumberOfWeeks = player.NumberOfWeeks;
                    waitingPlayer.TotalCost = player.TotalCost;
                    break;
                }
            }
            Loan loan = new Loan();
            loan.StartDate = DateTime.Now;
            loan.EndDate = loan.StartDate.AddDays(waitingPlayer.NumberOfWeeks * 7);
            loan.Ongoing = true;
            loan.Copy = copy;
            loan.Lender = copy.Owner;
            loan.Borrower = waitingPlayer;
            loan.IdLoan = Loan.createLoan(loan);
            updateCopyByIdLoan(loan);
            ((App)Application.Current).UserIdWithNewCopy = waitingPlayer.IdPlayer;
        }

        private void updateCopyByIdLoan(Loan loan)
        {
            loan.Copy.Loan = loan;
            Copy.updateLoanerCopy(loan.Copy);
        }

        private Player generatePlayerHaveCopy(int id)
        {
            waitingList = Booking.findAllBookingByIdVideoGame(id);
            foreach (Booking b in waitingList)
            {
                b.Player = (Player)Player.findPlayer(b.Player.IdPlayer);
            }
            Player selectedPlayer = SortBookingsByPriority();

            return selectedPlayer;
        }


        private Player SortBookingsByPriority()
        {
            List<Booking> sortedList = waitingList.OrderByDescending(booking => booking.Player.Credit)
                                                 .ThenBy(booking => booking.BookingDate)
                                                 .ThenBy(booking => booking.Player.RegistrationDate)
                                                 .ThenBy(booking => booking.Player.DateOfBirth)
                                                 .ThenBy(_ => Guid.NewGuid()) // Trier aléatoirement si tous les critères sont égaux
                                                 .ToList();

            return sortedList.First().Player;
        }

        private void dgLoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgLoan.SelectedItem is Loan l)
            {
                selectedLoan = l;
            }
        }
    }
}
