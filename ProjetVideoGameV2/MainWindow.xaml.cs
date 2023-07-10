using ProjectVideoGameV2.View;
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

        

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            
            Home_Page hp = new Home_Page();
            this.Content = hp;
               
            
        }

        private void Button_Register(object sender, RoutedEventArgs e)
        {

        }
    }

}

