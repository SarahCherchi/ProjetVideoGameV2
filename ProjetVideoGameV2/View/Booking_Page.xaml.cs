using ProjectVideoGameV2.View;
using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.Model.DAO;
using ProjetVideoGameV2.POCO;
using ProjetVideoGameV2.View.AdminInputDialog;
using System;
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
        private Loan loan = new Loan();
        private List<Copy> copies;
        private ICollectionView collectionView;
        private int totalCreditCost;
        private int numberOfWeeks;

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
            if (player.IdPlayer != copy.Owner.IdPlayer)
            {
                if (isAvailable(copy))
                {
                    calculationRentalCost(copy);
                    if (totalCreditCost <= player.Credit)
                    {
                        updatePlayer(copy);
                        createLoan(copy);
                        dgCopy.Items.Refresh();
                        MessageBox.Show($"Congratulations, you've just booked {copy.VideoGames.Name} on {copy.VideoGames.Console} for {numberOfWeeks} weeks.");
                    }
                    else
                    {
                        MessageBox.Show($"You cannot book for {numberOfWeeks} weeks. You have {player.Credit} credits, and the total cost is {totalCreditCost}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
            this.Content = hp;
        }
        
        private void createLoan(Copy copy)
        {
            loan.StartDate = DateTime.Now;
            loan.EndDate = loan.StartDate.AddDays(numberOfWeeks * 7);
            loan.Ongoing = true;
            loan.Copy = copy;
            loan.Lender = copy.Owner;
            loan.Borrower = player;
            loan.IdLoan = Loan.createLoan(loan);
            updateCopyByIdLoan(loan);
        }

       
        private void updateCopyByIdLoan(Loan loan)
        {
            loan.Copy.Loan = loan;
            Copy.updateLoanerCopy(loan.Copy);
        }

        private bool isAvailable(Copy copy)
        {
            if(copy.Available)
            {
                copy.Available = false;
                return true;
            }
            return false;
        }

        private void calculationRentalCost(Copy copy)
        {
            copy.VideoGames = VideoGames.FindVideoGames(videoGame.IdVideoGames);
            CreateLoanDialog createLoanDialog = new CreateLoanDialog(player, copy.VideoGames);
            createLoanDialog.ShowDialog();
            if (createLoanDialog.DialogResult == true)
            {
                numberOfWeeks = createLoanDialog.numberOfWeeks;
                totalCreditCost = numberOfWeeks * copy.VideoGames.CreditCost;
            }

        }

        private void updatePlayer(Copy copy)
        {
            player.Credit -= totalCreditCost;
            Player.updatePlayer(player);
            lb_credit.Content = player.Credit;
        }
    }
}
