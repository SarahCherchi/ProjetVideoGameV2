using ProjectVideoGameV2.View;
using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.Model.DAO;
using ProjetVideoGameV2.POCO;
using ProjetVideoGameV2.View;
using System.Windows;

namespace ProjetVideoGameV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            AdminDAO adminDAO = new AdminDAO();
            Administrator admin = new Administrator();
            adminDAO.Login(Username.Text, Password.Password);
            admin.Role = adminDAO.Login(Username.Text, Password.Password).Role;
            Admin_Page ap = new Admin_Page();
            this.Content = ap;
            if (admin.Role != true)
            {
                PlayerDAO playerDAO = new PlayerDAO();
                Player player = new Player();
                player.Login(Username.Text, Password.Password);
                player.IdPlayer = playerDAO.Login(Username.Text, Password.Password).IdPlayer;
                player.Pseudo = playerDAO.Login(Username.Text, Password.Password).Pseudo;
                player.Credit = playerDAO.Login(Username.Text, Password.Password).Credit;
                player.UserName = playerDAO.Login(Username.Text, Password.Password).UserName;
                player.Password = playerDAO.Login(Username.Text, Password.Password).Password;
                player.DateOfBirth = playerDAO.Login(Username.Text, Password.Password).DateOfBirth;
                player.LastDateBonus = playerDAO.Login(Username.Text, Password.Password).LastDateBonus;
                Home_Page hp = new Home_Page(player);
                this.Content = hp;
            }
        }

    }
}

