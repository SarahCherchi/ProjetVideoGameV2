using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
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
    public partial class Admin_ListOfLoan : UserControl
    {
        private List<Loan> loans;
        private ICollectionView collectionView;
        public Admin_ListOfLoan()
        {
            InitializeComponent();
            loans = Loan.findAllLoan();
            collectionView = CollectionViewSource.GetDefaultView(loans);
            dgLoans.ItemsSource = collectionView;

            foreach (var loan in loans)
            {
                loan.Copy = Copy.findCopy(loan.Copy.IdCopy);
                loan.Copy.VideoGames = VideoGames.FindVideoGames(loan.Copy.VideoGames.IdVideoGames);
            }
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            Admin_Page admin = new Admin_Page();
            this.Content = admin;
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
