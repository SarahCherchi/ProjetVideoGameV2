﻿using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
using System;
using System.Collections.Generic;
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
    public partial class Loan_Page : UserControl
    {
        private Player player;
        public Loan_Page(Player player)
        {
            InitializeComponent();
            this.player = player;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
        }

        private void Button_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Home_Page hp = new Home_Page(player);
            this.Content = hp;
        }

        private void Button_ReturnedCopy(object sender, RoutedEventArgs e)
        {
            
        }

        private void dgLoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}