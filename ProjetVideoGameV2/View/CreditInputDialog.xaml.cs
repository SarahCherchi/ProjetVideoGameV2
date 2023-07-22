using System.Windows;

namespace ProjetVideoGameV2.View
{
    public partial class CreditInputDialog : Window
    {
        public int NewCreditCost;

        public CreditInputDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtNewCreditCost.Text, out int newCreditCost))
            {
                if (newCreditCost >= 1 && newCreditCost <= 5)
                {
                    NewCreditCost = newCreditCost;
                    DialogResult = true; // This indicates that the user clicked the "Ok" button
                }
                else
                {
                    MessageBox.Show("Invalid credit cost. Please enter a number between 1 and 5.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid credit cost. Please enter a number.","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
