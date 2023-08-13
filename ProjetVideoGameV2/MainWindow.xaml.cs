using ProjectVideoGameV2.View;
using ProjetVideoGameV2.Model.DAO;
using ProjetVideoGameV2.POCO;
using ProjetVideoGameV2.View;
using System.Windows;

namespace ProjetVideoGameV2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Register(object sender, RoutedEventArgs e)
        {
            Register_Page rp = new Register_Page();
            this.Content = rp;
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            UserDAO userDAO = new UserDAO();
            User user = userDAO.Login(Username.Text, Password.Password);

            if (user != null)
            {
                if (user is Administrator admin)
                {
                    Admin_Page ap = new Admin_Page();
                    this.Content = ap;
                }
                if (user is Player player)
                {
                    Home_Page hp = new Home_Page(player);
                    this.Content = hp;
                }

            }
            else
            {
                MessageBox.Show("UserName or Password incorrect", "User not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}