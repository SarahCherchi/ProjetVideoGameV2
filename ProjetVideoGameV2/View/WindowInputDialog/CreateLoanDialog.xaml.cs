using ProjetVideoGameV2.POCO;
using System.Windows;

namespace ProjetVideoGameV2.View.AdminInputDialog
{
    
    public partial class CreateLoanDialog : Window
    {
        public int numberOfWeeks;
        public Player player;
        public VideoGames videoGames;
        public CreateLoanDialog(Player player, VideoGames vg)
        {
            InitializeComponent();
            this.player = player;
            this.videoGames = vg;
            lblGameName.Content = "Number of weeks : ";
            lblGameCost.Content = $"The price is {videoGames.CreditCost} credits/weeks";
            lbNameVideoGame.Content = $"{videoGames.Name}";
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtNumberOfWeeks.Text, out int numberOfWeeks))
            {
                if(numberOfWeeks > 0)
                {
                    int totalCreditCost = numberOfWeeks * videoGames.CreditCost;
                    if(totalCreditCost <= player.Credit) {
                        this.numberOfWeeks = numberOfWeeks;
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show($"You cannot book this game for {numberOfWeeks} weeks beacause you have {player.Credit} and the cost is {totalCreditCost} ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The number of weeks must be greater than 0 ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid number of weeks. Please enter a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
