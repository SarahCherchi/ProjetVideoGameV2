using ProjectVideoGameV2.View;
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
    
    public partial class Register_Page : UserControl
    {
        public Register_Page()
        {
            InitializeComponent();
        }
        private void Button_Confirm(object sender, RoutedEventArgs e)
        {
            if (Player.findPlayerByUsername(Username.Text) != null)
            {
                MessageBox.Show("Username already taken, choose another one ", "Username taken", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Player p = new Player();
                p.UserName = Username.Text;
                p.Password = Password.Password;
                p.DateOfBirth = DateTime.Parse(Birthday.Text);
                p.Pseudo = Pseudo.Text;
                p.Credit = 10;
                bool ok = Player.createPlayer(p);

                if (ok)
                {
                    MessageBox.Show("Your account has been created. You may now log in ", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Window register_page = Window.GetWindow(this);
                    MainWindow mainWindow = new MainWindow();

                    register_page.Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Account creation failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            Window home_page = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();

            home_page.Close();
            mainWindow.Show();
        }
    }
}
