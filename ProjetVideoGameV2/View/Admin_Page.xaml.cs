﻿using ProjetVideoGameV2.POCO;
using ProjetVideoGameV2.View.AdminInputDialog;
using System;
using System.Collections;
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
    /// <summary>
    /// Logique d'interaction pour Admin_Page.xaml
    /// </summary>
    public partial class Admin_Page : UserControl
    {
        public Admin_Page()
        {
            InitializeComponent();
            List<VideoGames> vg = VideoGames.FindAll();
            dgVideoGames.ItemsSource = vg;
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            VideoGames selectedGame = (VideoGames)dgVideoGames.SelectedItem;
            if (selectedGame == null)
            {
                MessageBox.Show("Please select a game to update.","Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CreditInputDialog inputDialog = new CreditInputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                selectedGame.CreditCost = inputDialog.NewCreditCost;
                VideoGames.UpdateCreditCost(selectedGame);

                List<VideoGames> vg = VideoGames.FindAll();
                dgVideoGames.ItemsSource = vg;

                MessageBox.Show("Credit cost updated successfully.");
            }
        }

        private void dgVideoGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Window home_page = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();

            home_page.Close();
            mainWindow.Show();

        }

        private void Button_ListOfUser(object sender, RoutedEventArgs e)
        {
            Admin_ListOfUserPage listOfUserPage = new Admin_ListOfUserPage();
            this.Content = listOfUserPage;
        }

        private void Button_AddVG(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                CreateVgInputDialog createVgDialog = new CreateVgInputDialog();
                if (createVgDialog.ShowDialog() == true)
                {
                    VideoGames newVg = new VideoGames();
                    newVg.Name = createVgDialog.txtNameVg.Text;
                    newVg.CreditCost = int.Parse(createVgDialog.txtCreditCost.Text);
                    newVg.Console = ((ComboBoxItem)createVgDialog.cmbConsole.SelectedItem).Content.ToString();

                    bool alreadyExist = VideoGames.FindVgByNameConsole(newVg);

                    if (!alreadyExist)
                    {
                        bool success = VideoGames.CreateVideoGame(newVg);

                        if (success)
                        {
                            MessageBox.Show("Video game added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                            List<VideoGames> vg = VideoGames.FindAll();
                            dgVideoGames.ItemsSource = vg;
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the video game.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This video game already exists on this console.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    break; //Pour éviter une boucle infinie si on veut fermer la fenêtre
                }
                
            }
        }
    }
}
