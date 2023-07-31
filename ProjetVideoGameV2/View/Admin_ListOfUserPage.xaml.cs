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
    public partial class Admin_ListOfUserPage : UserControl
    {
        private List<User> players;
        private ICollectionView collectionView;
        public Admin_ListOfUserPage()
        {
            InitializeComponent();
            players = Player.findAllPlayer();
            collectionView = CollectionViewSource.GetDefaultView(players);
            dgUsers.ItemsSource = collectionView;

            players.RemoveAll(player => player.Role == true);
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
