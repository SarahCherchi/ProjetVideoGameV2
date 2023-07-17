using ProjetVideoGameV2;
using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.POCO;
using ProjetVideoGameV2.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjectVideoGameV2.View
{

    public partial class Home_Page : UserControl
    {

        private Player player;

        public Home_Page(Player player)
        {
            InitializeComponent();
            //Player player = new Player();
            //player.addBirthday();
            this.player = player;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
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

        private void Button_Booking(object sender, RoutedEventArgs e)
        {
            if(player.Credit > 0)
            {
                Test test = new Test();
                this.Content = test;
            }
            else
            {
                MessageBox.Show("You cannot book a video game with 0 credits. Please rent a game first");
            }
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Window home_page = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();

            home_page.Close();
            mainWindow.Show();

        }
    }
}
