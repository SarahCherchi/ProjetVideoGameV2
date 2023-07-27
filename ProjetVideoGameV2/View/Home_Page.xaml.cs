﻿using ProjetVideoGameV2;
using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.Model.DAO;
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
        private VideoGames selectedVg;

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
            }

            dgVideoGames.ItemsSource = vg;

            dgVideoGames.SelectionChanged += dgVideoGames_SelectionChanged;

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
            if (dgVideoGames.SelectedItem is VideoGames selectedGame)
            {
                selectedVg = selectedGame;
            }
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            List<VideoGames> vg = VideoGames.FindVideoGamesByName(nameSearch.Text);
            dgVideoGames.ItemsSource = vg;
            foreach (var game in vg)
            {
                game.NumberOfCopy = VideoGames.nbrCopyAvailable(game.IdVideoGames);
            }
        }

        private void Button_Renting(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to add your copy?", "Add a copy", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Copy copy = new Copy();

                copy.VideoGames = selectedVg;
                copy.Owner = player;

                bool success = Copy.createCopy(copy);

                if (success)
                {
                    MessageBox.Show("Renting successful!", "Renting", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Renting failed!", "Renting", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            Home_Page home_Page = new Home_Page(player);
            Content = home_Page;
            
        }

        private void Button_Booking(object sender, RoutedEventArgs e)
        {
            if(player.Credit > 0)
            {
                Booking_Page book = new Booking_Page(selectedVg,player);
                this.Content = book;
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
