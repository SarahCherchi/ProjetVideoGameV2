using ProjetVideoGameV2;
using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.POCO;
using ProjetVideoGameV2.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ProjectVideoGameV2.View
{

    public partial class Home_Page : UserControl
    {

        private Player player;

        public Home_Page(Player player)
        {
            InitializeComponent();
            this.player = player;
            
            player.addBirthdayBonus();
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
            List<VideoGames> vg = VideoGames.FindAll();

            foreach (var game in vg)
            {
                game.NumberOfCopy = VideoGames.nbrCopyAvailable(game.IdVideoGames);
                game.IsAvailable = Copy.IsAvailable(game.IdVideoGames);
            }

            dgVideoGames.ItemsSource = vg;

            Loaded += Home_Page_Loaded;
        }

        private void Home_Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.3);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();

            ShowBirthdayMessage();
        }

        private void ShowBirthdayMessage()
        {
            if (player.bonusReceived)
            {
                MessageBox.Show("Congratulations! You have won 2 credits for your birthday!", "Birthday Bonus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
