using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace ProjetVideoGameV2.View
{
   
    public partial class UserCopiesBook : UserControl
    {
        private Player player;
        private Copy copies;
        private List<Loan> loans;
        private ICollectionView collectionView;
        public UserCopiesBook(Player player, Copy copy)
        {
            InitializeComponent();
            this.player = player;
            this.copies = copy;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
            loans = Loan.findAllLoanByIdLender(player.IdPlayer,copies.IdCopy);
            collectionView = CollectionViewSource.GetDefaultView(loans);
            dgCopiesBooking.ItemsSource = collectionView;

            foreach (var loan in loans)
            {
                loan.Borrower = Player.findPlayer(loan.Borrower.IdPlayer);
            }

        }

        private void Button_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Copies_Page cp = new Copies_Page(player);
            this.Content = cp;
        }

        private void dgCopiesBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
