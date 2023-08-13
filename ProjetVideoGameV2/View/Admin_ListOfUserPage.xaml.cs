using ProjetVideoGameV2.POCO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace ProjetVideoGameV2.View
{
    public partial class Admin_ListOfUserPage : UserControl
    {
        private List<User> players;
        private ICollectionView collectionView;
        public Admin_ListOfUserPage()
        {
            InitializeComponent();
            initDgPlayer();
        }

        private void initDgPlayer()
        {
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
