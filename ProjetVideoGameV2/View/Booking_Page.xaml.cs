using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProjetVideoGameV2.View
{
    public partial class Booking_Page : UserControl
    {
        private VideoGames videoGame;
        private List<Copy> copies;
        private ICollectionView collectionView;

        public Booking_Page(VideoGames vg)
        {
            InitializeComponent();
            videoGame = vg;
            
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

        private void Button_GoBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
