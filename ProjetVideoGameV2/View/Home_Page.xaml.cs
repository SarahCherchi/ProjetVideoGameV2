﻿using ProjectVideoGameV2.POCO;
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