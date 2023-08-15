using ProjetVideoGameV2;
using ProjetVideoGameV2.POCO;
using ProjetVideoGameV2.View;
using ProjetVideoGameV2.View.AdminInputDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ProjectVideoGameV2.View
{

    public partial class Home_Page : UserControl
    {
        private Booking booking = new Booking();
        private Copy copy = new Copy();
        private Player player;
        private Player waitingPlayer;
        private VideoGames selectedVg;
        private List<Booking> waitingList = new List<Booking>();
        private bool ok;
        private int userIdWithNewCopy;
        private int numberOfWeeks;

        public Home_Page(Player player)
        {
            InitializeComponent();
            this.player = player;
            verificationDateOfBirth();
            verificationUserIdWithNewCopy();
            lb_pseudo.Content = player.Pseudo;
            lb_credit.Content = player.Credit;
            initDgVideoGames();
          
        }

        private void verificationDateOfBirth()
        {
            ok = player.addBirthdayBonus();
            if (ok)
            {
                Loaded += Home_Page_Loaded;
            }
        }

        private void verificationUserIdWithNewCopy()
        {
            userIdWithNewCopy = ((App)Application.Current).UserIdWithNewCopy;

            if (userIdWithNewCopy == player.IdPlayer)
            {
                Loaded += Home_Page_Loaded;
            }
        }

        private void initDgVideoGames()
        {
            List<VideoGames> vg = VideoGames.FindAll();

            foreach (var game in vg)
            {
                game.NumberOfCopy = VideoGames.nbrCopyAvailable(game.IdVideoGames);
            }

            dgVideoGames.ItemsSource = vg;

            dgVideoGames.SelectionChanged += dgVideoGames_SelectionChanged;
        }

        private void Home_Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            if (ok)
            {
                showBirthdayMessage();
            }
            else if(userIdWithNewCopy == player.IdPlayer)
            {
                showBookingMessage();
            }
        }

        private void showBirthdayMessage()
        {
            if (player.bonusReceived)
            {
                MessageBox.Show("Congratulations! You have won 2 credits for your birthday!", "Birthday Bonus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void showBookingMessage()
        {
            MessageBox.Show("Congratulations! You have received a new loan for a game from the waiting list.", "New Loan", MessageBoxButton.OK, MessageBoxImage.Information);
            ((App)Application.Current).UserIdWithNewCopy = -1;
        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            Home_Page home = new Home_Page(player);
            Content = home;
        }

        private void Button_Loan(object sender, RoutedEventArgs e)
        {
            Loan_Page loan = new Loan_Page(player);
            this.Content = loan;
        }

        private void Button_Copies(object sender, RoutedEventArgs e)
        {
            Copies_Page copies = new Copies_Page(player);
            this.Content = copies;
        }

        private void Button_BookingList(object sender, RoutedEventArgs e)
        {
            BookingList_Page bookingList = new BookingList_Page(player);
            this.Content = bookingList;
        }

        private void Button_History(object sender, RoutedEventArgs e)
        {
            History_Page history = new History_Page(player);
            this.Content = history;
        }

        private void Button_Account(object sender, RoutedEventArgs e)
        {
            AccountInfo account = new AccountInfo(player);
            this.Content = account;
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            Window home_page = Window.GetWindow(this);
            MainWindow mainWindow = new MainWindow();

            home_page.Close();
            mainWindow.Show();

        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            List<VideoGames> vg = VideoGames.FindVideoGamesByName(nameSearch.Text);
            dgVideoGames.ItemsSource = vg;
            foreach (var game in vg)
            {
                game.NumberOfCopy = VideoGames.nbrCopyAvailable(game.IdVideoGames);
            }
        }

        private void Button_Booking(object sender, RoutedEventArgs e)
        {
            if (isNumberOfCopies())
            {
                if (isEnoughCredit(player, selectedVg))
                {
                    Booking_Page book = new Booking_Page(selectedVg, player);
                    this.Content = book;
                }
                else
                {
                    MessageBox.Show("You don't have enough credits for book this game. Please lend one of your games first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (isEnoughCredit(player, selectedVg))
                {
                    if (isAllowedToWaitingList(selectedVg))
                    {
                        MessageBoxResult result = MessageBox.Show($"Do you want to get on the waiting list for {selectedVg.Name} ?", "New Booking", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            calculationRentalCost();
                            if (numberOfWeeks <= 0 || numberOfWeeks.Equals(null))
                            {
                                return;
                            }
                            createNewBooking(selectedVg);
                            MessageBox.Show($"Congratulations you are placed on the waiting list", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot are placed on the waiting list a second time", "Error Booking", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("You don't have enough credit to be place to the waiting list. Please lend one of your games first");
                }
            }
        }

        private bool isNumberOfCopies()
        {
            return selectedVg.NumberOfCopy > 0;
        }

        private bool isEnoughCredit(Player player, VideoGames videoGames)
        {
            if (player.Credit > 0 && player.Credit >= videoGames.CreditCost)
            {
                return true;
            }
            return false;
        }

        public bool isAllowedToWaitingList(VideoGames videoGames)
        {
            List<Booking> playerBookings = Booking.findAllBookingByIdVideoGame(videoGames.IdVideoGames).Where(booking => booking.Player.IdPlayer == player.IdPlayer).ToList();

            return playerBookings.Count == 0;
        }

        private void calculationRentalCost()
        {
            CreateLoanDialog createLoanDialog = new CreateLoanDialog(player, selectedVg);
            createLoanDialog.ShowDialog();
            if (createLoanDialog.DialogResult == true)
            {
                numberOfWeeks = createLoanDialog.numberOfWeeks;
            }

        }

        private void createNewBooking(VideoGames videoGames)
        {
            booking.BookingDate = DateTime.Now;
            booking.VideoGames = videoGames;
            booking.Player = player;
            booking.NumberOfWeeks = numberOfWeeks;
            bool success = Booking.createBooking(booking);
            if (success)
            {
                waitingList.AddRange(Booking.findAllBookingByIdVideoGame(selectedVg.IdVideoGames));
            }
            else
            {
                MessageBox.Show("Error at the creation of the booking", "Error Booking", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Renting(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to add your copy?", "Add a copy", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (isCreateCopy())
                {
                    MessageBox.Show("Renting successful!", "Renting", MessageBoxButton.OK, MessageBoxImage.Information);
                    selectedVg.NumberOfCopy = VideoGames.nbrCopyAvailable(selectedVg.IdVideoGames);
                    waitingList = Booking.findAllBookingByIdVideoGame(selectedVg.IdVideoGames);
                    if (isNumberOfWeek())
                    {
                        copy.IdCopy = Copy.findAllCopyByIdVG(selectedVg.IdVideoGames).Last().IdCopy;
                        AllocateCopyToWaitingPlayer(selectedVg, copy);
                        if (isDeleteBooking())
                        {
                            updatePlayer();
                            MessageBox.Show("Your copy has just been assigned to a player on the waiting list", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("An error has occurred", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    Home_Page home_Page = new Home_Page(player);
                    Content = home_Page;
                }
                else
                {
                    MessageBox.Show("Renting failed!", "Renting", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private bool isCreateCopy()
        {
            copy.VideoGames = selectedVg;
            copy.Owner = player;
            return Copy.createCopy(copy);
        }

        private bool isNumberOfWeek()
        {
            return selectedVg.NumberOfCopy == 1 && waitingList.Count > 0;
        }

        private void AllocateCopyToWaitingPlayer(VideoGames VideoGame, Copy copy)
        {
            waitingPlayer = generatePlayerHaveCopy(VideoGame.IdVideoGames);
            booking = Booking.findBookingByVideoGameAndUser(waitingPlayer.IdPlayer, VideoGame.IdVideoGames);
            Loan loan = new Loan();
            loan.StartDate = DateTime.Now;
            loan.EndDate = loan.StartDate.AddDays(booking.NumberOfWeeks * 7);
            loan.Ongoing = true;
            loan.Copy = copy;
            loan.Lender = copy.Owner;
            loan.Borrower = waitingPlayer;
            loan.IdLoan = Loan.createLoan(loan);
            updateCopyByIdLoan(loan);
            ((App)Application.Current).UserIdWithNewCopy = waitingPlayer.IdPlayer;
        }

        private Player generatePlayerHaveCopy(int id)
        {
            waitingList = Booking.findAllBookingByIdVideoGame(id);
            foreach (Booking b in waitingList)
            {
                b.Player = (Player)Player.findPlayer(b.Player.IdPlayer);
            }
            Player selectedPlayer = SortBookingsByPriority();

            return selectedPlayer;
        }


        private Player SortBookingsByPriority()
        {
            List<Booking> sortedList = waitingList.OrderByDescending(booking => booking.Player.Credit)
                                                 .ThenBy(booking => booking.BookingDate)
                                                 .ThenBy(booking => booking.Player.RegistrationDate)
                                                 .ThenBy(booking => booking.Player.DateOfBirth)
                                                 .ThenBy(_ => Guid.NewGuid()) // Trier aléatoirement si tous les critères sont égaux
                                                 .ToList();

            return sortedList.First().Player;
        }

        private void updateCopyByIdLoan(Loan loan)
        {
            loan.Copy.Loan = loan;
            Copy.updateLoanerCopy(loan.Copy);
        }

        private bool isDeleteBooking()
        {
            return Booking.deleteBookingByIdUserAndIdVideoGame(waitingPlayer.IdPlayer, selectedVg.IdVideoGames);
        }

        private void updatePlayer()
        {
            int totalCost = booking.NumberOfWeeks * selectedVg.CreditCost;
            player.Credit = player.Credit + totalCost;
            Player.updatePlayer(player);
            waitingPlayer.Credit = waitingPlayer.Credit - totalCost;
            Player.updatePlayer(waitingPlayer);
        }

        private void dgVideoGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVideoGames.SelectedItem is VideoGames selectedGame)
            {
                selectedVg = selectedGame;
            }
        }
    }
}
