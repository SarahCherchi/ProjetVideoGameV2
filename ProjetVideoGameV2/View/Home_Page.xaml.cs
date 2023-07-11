using ProjetVideoGameV2.Model.Dao;
using ProjetVideoGameV2.POCO;
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
