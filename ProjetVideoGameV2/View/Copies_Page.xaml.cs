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
            List<Copy> cp = Copy.findAllCopiesByUser(player.IdPlayer);
            dgCopies.ItemsSource = cp;
            
            foreach(Copy copy in cp)
            {
                copy.VideoGames = VideoGames.FindVideoGames(copy.VideoGames.IdVideoGames);
                copy.Available = Copy.IsAvailable(copy.IdCopy);
            }

        }

        private void Button_ViewMore(object sender, RoutedEventArgs e)
        {
            UserCopiesBook userCopiesBook = new UserCopiesBook(player,selectedCopy);
            this.Content = userCopiesBook;
        }

        private void Button_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page(player);
            this.Content = hp;
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
