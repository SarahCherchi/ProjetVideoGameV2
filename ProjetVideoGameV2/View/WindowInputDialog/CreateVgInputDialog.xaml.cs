using System.Windows;
using System.Windows.Controls;

namespace ProjetVideoGameV2.View.AdminInputDialog
{
    public partial class CreateVgInputDialog : Window
    {
        public CreateVgInputDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string nameVg = txtNameVg.Text;
            if (string.IsNullOrEmpty(nameVg))
            {
                MessageBox.Show("Please enter the name of the video game.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (int.TryParse(txtCreditCost.Text, out int creditCost))
            {
                if (creditCost >= 1 && creditCost <= 5)
                {
                    if (cmbConsole.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a console.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Invalid credit cost. Please enter a number between 1 and 5.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid credit cost. Please enter a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
