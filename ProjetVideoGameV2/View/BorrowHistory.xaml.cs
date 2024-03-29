﻿using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace ProjetVideoGameV2.View
{
   
    public partial class BorrowHistory : UserControl
    {
        private Player player;
        private Copy copies;
        private List<Loan> loans;
        private ICollectionView collectionView;
        public BorrowHistory(Player player, Copy copy)
        {
            InitializeComponent();
            this.player = player;
            this.copies = copy;
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
            initDgBorrowLoan();
        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            Home_Page home = new Home_Page(player);
            Content = home;
        }

        private void Button_Loan(object sender, RoutedEventArgs e)
        {
            Loan_Page loan = new Loan_Page(player);
            this.Content = loan;
        }

        private void Button_Copies(object sender, RoutedEventArgs e)
        {
            Copies_Page copies = new Copies_Page(player);
            this.Content = copies;
        }

        private void Button_Account(object sender, RoutedEventArgs e)
        {
            AccountInfo account = new AccountInfo(player);
            this.Content = account;
        }

        private void Button_BookingList(object sender, RoutedEventArgs e)
        {
            BookingList_Page bookingList = new BookingList_Page(player);
            this.Content = bookingList;
        }

        private void Button_History(object sender, RoutedEventArgs e)
        {
            History_Page history = new History_Page(player);
            this.Content = history;
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Window home_page = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();

            home_page.Close();
            mainWindow.Show();

        }

        private void initDgBorrowLoan()
        {
            loans = Loan.findAllLoanByIdLender(player.IdPlayer, copies.IdCopy);
            collectionView = CollectionViewSource.GetDefaultView(loans);
            dgCopiesBooking.ItemsSource = collectionView;

            foreach (var loan in loans)
            {
                loan.Borrower = (Player)Player.findPlayer(loan.Borrower.IdPlayer);
            }
        }

        private void dgCopiesBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
