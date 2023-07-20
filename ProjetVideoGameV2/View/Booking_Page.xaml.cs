using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProjetVideoGameV2.View
{
    public partial class Booking_Page : UserControl
    {
        private VideoGames videoGame;
        private Player player;
        private List<Copy> copies;
        private ICollectionView collectionView;

        public Booking_Page(VideoGames vg, Player player)
        {
            InitializeComponent();
            this.videoGame = vg;
            this.player = player;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;

            RefreshData();
        }

        private void RefreshData()
        {
            if (chkCopy.IsChecked == false)
            {
                copies = Copy.findAllCopyByIdVG(videoGame.IdVideoGames);
            }
            else
            {
                copies = VideoGames.CopyAvailable(videoGame.IdVideoGames);
            }

            collectionView = CollectionViewSource.GetDefaultView(copies);
            dgCopy.ItemsSource = collectionView;

            foreach (var copy in copies)
            {
                copy.Available = Copy.IsAvailable(copy.IdCopy);

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void dgCopy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_ConfirmBooking_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Button_BookingCopy(object sender, RoutedEventArgs e)
        {
            Copy copy = dgCopy.SelectedItem as Copy;
            if (player.IdPlayer != copy.Owner.IdPlayer) //CORRIGER LE IF 
            {
                if (copy.Available)
                {
                    copy.Available = false;
                    //Copy.updateLoanerCopy(copy);
                    videoGame = VideoGames.FindVideoGames(videoGame.IdVideoGames);
                    copy.VideoGames = videoGame;
                    player.Credit = player.Credit - copy.VideoGames.CreditCost;
                    Player.updatePlayer(player);
                    lb_credit.Content = player.Credit;
                    dgCopy.Items.Refresh();
                    MessageBox.Show($"Congratulations, you've just booked {copy.VideoGames.Name} on {copy.VideoGames.Console}");
                }
                else
                {
                    MessageBox.Show("This copy is already booked.");
                }
            }
            else
            {
                MessageBox.Show("You can't book your own copy !");
            }
        }


        private void Button_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page(player);
        }
    }
}
