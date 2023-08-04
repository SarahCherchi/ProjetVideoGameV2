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
    public partial class AccountInfo : UserControl
    {
        private Player player;

        public AccountInfo(Player player)
        {
            InitializeComponent();
            this.player = player;
            Username.Text = player.UsernameString;
            Credit.Text = player.Credit.ToString();
            Pseudo.Text = player.Pseudo;
            DateofBirth.Text = player.DateOfBirthString;
            RegistrationDate.Text = player.RegistrationDateString;
        }
    }
}
