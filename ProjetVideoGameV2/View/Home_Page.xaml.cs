using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjectVideoGameV2.View
{

    public partial class Home_Page : UserControl
    {

        public Home_Page()
        {
            InitializeComponent();
            List<VideoGames> vg = VideoGames.FindAll();

            foreach (var game in vg)
            {
                game.NumberOfCopy = VideoGames.CopyAvailable(game.IdVideoGames);
                if(game.NumberOfCopy > 0)
                {
                    game.IsAvailable = true;
                }
                else
                {
                    game.IsAvailable = false;
                }
                //game.Available = copyAvailable ? "Yes" : "No";
            }


            dgVideoGames.ItemsSource = vg;
        }

        private void dgVideoGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Renting(object sender, RoutedEventArgs e)
        {

        }
    }
}
