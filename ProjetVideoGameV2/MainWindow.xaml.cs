using ProjectVideoGameV2.View;
using ProjetVideoGameV2.POCO;
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
            /*Register_Page rp = new Register_Page();
            this.Content = rp;*/
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            Player player = new Player();
            player.UserName = Username.Text;
            player.Password = Password.Text;
            player = player.loginPlayer(player);
            /*Home_Page hp = new Home_Page(player);
            this.Content = hp;*/
        }
    }

}

