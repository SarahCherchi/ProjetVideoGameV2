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
                loan.Lender = Player.findPlayer(loan.Lender.IdPlayer);
                loan.Copy = Copy.findCopy(loan.Copy.IdCopy);
                loan.Copy.VideoGames = VideoGames.FindVideoGames(loan.Copy.VideoGames.IdVideoGames);
            }
        }

        private void Button_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page(player);
            this.Content = hp;
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
                refreshData();
            }
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
