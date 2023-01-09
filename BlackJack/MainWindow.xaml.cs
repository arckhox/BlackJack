//Datum: Check Github, for commits and pushes
//Auteur: Arsalan Khosrojerdi
//Discription: C# code van de XAML voor de MainWindow

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
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
using System.Windows.Threading;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// Initiates all the variables and componets(dispatcherTimers included).
    /// Makes the game ready to play
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Instance of the Game Class
        /// </summary>
        private Game blackJack;
        /// <summary>
        /// The timer used for displaying the cards when dealed
        /// </summary>
        private DispatcherTimer dispatcherTimer;
        /// <summary>
        /// The timer used for displaying the Time below the screen in the format (hh:mm:ss)
        /// </summary>
        private DispatcherTimer time;
        /// <summary>
        /// A list which contains the images of all the cards for the player to display on the screen
        /// </summary>
        private List<Image> playerCardsImages;
        /// <summary>
        /// A list which contains the images of all the cards for the dealer to display on the screen
        /// </summary>
        private List<Image> dealerCardsImages;
        /// <summary>
        /// A list containing a formatted string for each card of the dealer. The string contains the suit and the number of the card.
        /// </summary>
        private List<String> dealerCards;
        /// <summary>
        /// A list containing a formatted string for each card of the player. The string contains the suit and the number of the card.
        /// </summary>
        private List<String> playerCards;
        /// <summary>
        /// Number of the current round in action.
        /// </summary>
        private int roundNumber = 0;
        /// <summary>
        /// A list containing information about previous played rounds.
        /// </summary>
        private List<RoundInfo> historyList;
        public MainWindow()
        {
            InitializeComponent();
            playerCardsImages = new List<Image>();
            dealerCardsImages = new List<Image>();
            dealerCards = new List<string>();
            playerCards = new List<string>();
            historyList = new List<RoundInfo>();
            int deckCount = 1;
            blackJack = new Game(deckCount);
            blackJack.initiateGame();
            // to show the time
            time = new System.Windows.Threading.DispatcherTimer();
            time.Tick += new EventHandler(timeTick);
            time.Interval = new TimeSpan(0, 0, 1);
            time.Start();
            // to deal the cards
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        /// <summary>
        /// The function that gets called when the dealButton is clicked.
        /// it hides the score labels, status bar.
        /// it also changes the score of the player and the dealer and changes the balance of the player to the right amount.
        /// </summary>
        private void dealButton_Click(object sender, RoutedEventArgs e)
        {
            roundNumber++;
            int playerBet = Convert.ToInt32(playerBetLabel.Content);
            hideScoreLabels(true);
            blackJack.changeBalance(playerBet * (-1));
            updateBalance();
            string playerbetValue = playerBet.ToString();
            restartGame();
            pickCard(true); // pass true if the card picked is for the dealer
            pickCard(false);
            pickCard(false);
            playerScoreLabel.Content = blackJack.getPlayerScore();
            dealerScoreLabel.Content = blackJack.getDealerScore();
            updateBalance();
            dealButton.IsEnabled = false;
            playerBetSlider.IsEnabled = false;
            playerBetLabel.Content = playerbetValue;
            statusTextLabel.Background = Brushes.Black;
            statusTextLabel.Content = "Hit Or Stand? ;)";
            if (blackJack.getPlayerScore() == 21)
            {
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                standButton_Click(null, null);
            }
            // check if the player has enough balance to double down the bet(already subtracted from his balance once)
        }
        /// <summary>
        /// The function that gets called when the hitButton is clicked.
        /// hits a card. it hides the score labels, status bar.
        /// it also changes the score of the player.
        /// </summary>
        private void hitButton_Click(object sender, RoutedEventArgs e)
        {
            hideScoreLabels(true);
            pickCard(false);
            playerScoreLabel.Content = blackJack.getPlayerScore();
            hitButton.IsEnabled = false;
            standButton.IsEnabled = false;
            if (blackJack.getPlayerScore() > 21)
            {  
                gameLost();
            }
            else if (blackJack.getPlayerScore() == 21)
            {
                
                standButton_Click(null, null);
            }
            else
            {
                hitButton.IsEnabled = true;
                standButton.IsEnabled = true;
            }
        }
        /// <summary>
        /// The function that gets called when the hitButton is clicked.
        /// it hides the score labels, status bar.
        /// hits the cards for the dealer. Checks if the player won/lost the round.
        /// </summary>
        private void standButton_Click(object sender, RoutedEventArgs e)
         {
            if (blackJack.getPlayerScore() > 21)
            {
                return;
            }
            hideScoreLabels(true);
            while (blackJack.getDealerScore() < 16)
            {
                pickCard(true);
            }
            dealerScoreLabel.Content = blackJack.getDealerScore();
            if (blackJack.getDealerScore() > 21 && (blackJack.getPlayerScore() <= blackJack.getDealerScore()))
            {
                gameWon();
            }
            else if (blackJack.getDealerScore() > blackJack.getPlayerScore())
            {
                gameLost();
            }
            else
            {
                if (blackJack.getDealerScore() == blackJack.getPlayerScore()) 
                {
                    string playerBet = playerBetLabel.Content.ToString();
                    historyTextLabel.Content = "Last Round Info: Push " + playerBet + " With Player " + blackJack.getPlayerScore() + " and Dealer " + blackJack.getDealerScore();
                    statusTextLabel.Content = "Push!";
                    statusTextLabel.Background = Brushes.DarkGray;
                    blackJack.changeBalance(Convert.ToInt32(playerBetLabel.Content));
                    hitButton.IsEnabled = false;
                    standButton.IsEnabled = false;
                    playerBetSlider.IsEnabled = true;
                    updateBalance();
                    playerBetLabel.Content = "0";
                    playerBetSlider.Value = 0;
                    return;
                }
                gameWon();
            }
        }
        /// <summary>
        /// The function that gets called when the doubleDownButton is clicked.
        /// changes the player balance to the right amount(Due to doubling down). it hides the score labels, status bar.
        /// hits one card for the player and automatically stands.
        /// </summary>
        private void doubleDownButton_Click(object sender, RoutedEventArgs e)
        {
            int playerBet = Convert.ToInt32(playerBetLabel.Content.ToString());
            blackJack.changeBalance(playerBet * -1);
            playerBetLabel.Content = playerBet * 2;
            doubleDownButton.IsEnabled = false;

            hitButton_Click(null, null);
            standButton_Click(null,null);

        }
        /// <summary>
        /// Checks if the player's balance is enough to place the bet
        /// </summary>
        /// <param name="playerBet">The amount(int) of the bet placed by the player</param>
        /// <returns><c>true</c> if the player has enough balance to place the bet, otherwise <c>false</c></returns>
        private bool checkPlayerBalance(int playerBet)
        {
            int playerBalance = blackJack.getBalance();
            // the player must afford the bet value already paid again.
            if (playerBalance >= playerBet) 
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// This function is called when the round is lost.
        /// disables the buttons and slider. shows a text to the player saying he lost.
        /// set the players bet back to 0 and updates the balance of the player.
        /// </summary>
        private void gameLost()
        {
            
            if (blackJack.getBalance() == 0)
            {
                dealButton.IsEnabled = false;
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                playerBetSlider.IsEnabled = false;
                statusTextLabel.Content = "You lost all your money! Start a new game to continue";
                playerBetLabel.Content = "0";
                playerBetSlider.Value = 0;
            }
            else
            {
                string playerBet = playerBetLabel.Content.ToString();
                statusTextLabel.Content = "You lost " + playerBet;
                updateHistory(false, playerBet);
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                playerBetSlider.IsEnabled = true;
                playerBetLabel.Content = "0";
                playerBetSlider.Value = 0;
            }
            statusTextLabel.Background = Brushes.DarkRed;
            updateBalance();
        }
        /// <summary>
        /// This function is called when the round is Won.
        /// disables the buttons and slider. shows a text to the player saying he lost.
        /// set the players bet back to 0 and updates the balance of the player.
        /// </summary>
        private void gameWon()
        {
            string playerBet = playerBetLabel.Content.ToString();
            statusTextLabel.Content = "You Won " + playerBet;
            updateHistory(true, playerBet);
            statusTextLabel.Background = Brushes.DarkGreen;
            blackJack.changeBalance((Convert.ToInt32(playerBetLabel.Content)) * 2); // als de player wint dan krijgt hij 2x zijn bet terug
            updateBalance();
            playerBetLabel.Content = "0";
            playerBetSlider.Value = 0;
            hitButton.IsEnabled = false;
            standButton.IsEnabled = false;
            playerBetSlider.IsEnabled = true;
        }
        /// <summary>
        /// Resets all the values and initiates the game again making it ready to start a complete new game.
        /// </summary>
        private void restartGame()
        {
            blackJack.initiateGame();
            dealButton.IsEnabled = false;
            hitButton.IsEnabled = false;
            standButton.IsEnabled = false;
            playerBetSlider.IsEnabled = true;
            dealerListView.Items.Clear();
            playerListView.Items.Clear();
            historyList.Clear();
            statusTextLabel.Content += " Start Betting To Start Next Round";
            updateBalance(); 
            playerBetLabel.Content = "0";
            playerBetSlider.Value = 0;
            playerCardsView.Children.Clear();
            dealerCardsView.Children.Clear();
            doubleDownButton.IsEnabled = false;
        }
        /// <summary>
        /// Checks if the value chosen by the player via the slider is 10% of the players total balance and allows the player to place the bet if so.
        /// </summary>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            playerBetLabel.Content = Math.Round(((playerBetSlider.Value * 0.1) * blackJack.getBalance()),MidpointRounding.ToNegativeInfinity);
            if (playerBetSlider.Value >= 1)
            {
                dealButton.IsEnabled = true;
            }
            else
            {
                dealButton.IsEnabled = false;
            }
        }
        /// <summary>
        /// This function is called when the newGameButton is clicked. It restarts the game back to the beginning state.
        /// </summary>
        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Stop();
            }
            
            blackJack.resetBalance();
            restartGame();
            statusTextLabel.Content = " New Game started Start betting";
            statusTextLabel.Background = Brushes.Black;
            playerScoreLabel.Content = 0;
            dealerScoreLabel.Content = 0;
        }
        /// <summary>
        /// Picks a card for the dealer or the player depending on the argument passed to it.
        /// </summary>
        /// <param name="isForDealer"><c>true</c> if the card should be picked for the dealer otherwise <c>false</c> for the player</param>
        private void pickCard(bool isForDealer)
        {
            if (isForDealer)
            {
                dealerCards.Add(blackJack.pickCardForDealer());
                string tempCardNumber = blackJack.getLastDealerCardNumber();
                string tempCardSuit = blackJack.getLastDealerCardSuit();
                string tempPicName = tempCardNumber + tempCardSuit;
                Image tempImg = new Image();
                Uri source = new Uri("resources/Cards/" + tempPicName + ".svg.png", UriKind.Relative);
                tempImg.Source = new BitmapImage(source);
                tempImg.MaxWidth = 50;
                tempImg.Margin = new Thickness(5);
                dealerCardsImages.Add(tempImg);
                dispatcherTimer.Start();
                return;
            }
            else
            {
                playerCards.Add(blackJack.pickCardForPlayer());
                string tempCardNumber = blackJack.getLastPlayerCardNumber();
                string tempCardSuit = blackJack.getLastPlayerCardSuit();
                string tempPicName = tempCardNumber + tempCardSuit;
                Image tempImg = new Image();
                Uri source = new Uri("resources/Cards/" + tempPicName + ".svg.png", UriKind.Relative);
                tempImg.Source = new BitmapImage(source);
                tempImg.MaxWidth = 50;
                tempImg.Margin = new Thickness(5);
                playerCardsImages.Add(tempImg);
                dispatcherTimer.Start();
                return;
            }
        }
        /// <summary>
        /// Changes the time each second.
        /// </summary>
        private void timeTick(object sender, EventArgs e)
        {
            timeTextLabel.Content = DateTime.Now.ToString("hh:mm:ss");
        }
        /// <summary>
        /// timer used for dealing the cards and displaying them on the screen.
        /// </summary>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            // Updating the Label which displays the current second
            if (playerCardsImages.Count() != 0)
            {
                playerListView.Items.Add(playerCards.First());
                playerCards.Remove(playerCards.First());
                playerCardsView.Children.Add(playerCardsImages.First());
                playerCardsImages.Remove(playerCardsImages.First());
                return;
            }else if (dealerCardsImages.Count() != 0)
            {
                dealerListView.Items.Add(dealerCards.First());
                dealerCards.Remove(dealerCards.First());
                dealerCardsView.Children.Add(dealerCardsImages.First());
                dealerCardsImages.Remove(dealerCardsImages.First());
                return;
            }
            else
            {
                dispatcherTimer.Stop();
                if (statusTextLabel.Background == Brushes.Black)
                {
                    hitButton.IsEnabled = true;
                    standButton.IsEnabled = true;
                    int playerBet = Convert.ToInt32(playerBetLabel.Content);
                    if (checkPlayerBalance(playerBet))
                    {
                        doubleDownButton.IsEnabled = true;
                    }
                }
                hideScoreLabels(false);
            }
            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }
        /// <summary>
        /// Updates the label that shows the balance of the player.
        /// </summary>
        private void updateBalance()
        {
            playerBalanceLabel.Content = blackJack.getBalance();
        }
        /// <summary>
        /// adds information about the past round to the historical list and shows it on the screen.
        /// </summary>
        /// <param name="playerBet">The amount of the bet placed by the player in string format</param>
        /// <param name="playerWon"><c>true</c> if player won the game otherwise <c>false</c></param>
        private void updateHistory(bool playerWon, string playerBet)
        {
            RoundInfo tempInfo;
            if (playerWon)
            {
                historyTextLabel.Content = "Last Round Info: You Won " + playerBet + " With Player " + blackJack.getPlayerScore() + " and Dealer " + blackJack.getDealerScore();
                tempInfo = new RoundInfo(roundNumber,Convert.ToInt32(playerBet), blackJack.getPlayerScore(), blackJack.getDealerScore(), true);
            }
            else
            {
                historyTextLabel.Content = "Last Round Info: You lost " + playerBet + " With Player " + blackJack.getPlayerScore() + " and Dealer " + blackJack.getDealerScore();
                tempInfo = new RoundInfo(roundNumber, Convert.ToInt32(playerBet), blackJack.getPlayerScore(), blackJack.getDealerScore(), false);
            }
            historyList.Add(tempInfo);
        }
        /// <summary>
        /// This function is called when the history label is clicked. It shows a message to the player containing information on the previous 10 played rounds.
        /// </summary>
        private void historyTextLabel_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            for (int i = (historyList.Count - 1); i >= 0; i--)
            {
                counter++;
                if (counter!=10)
                {
                    sb.AppendLine(historyList.ElementAt(i).getString());
                }
                else
                {
                    break;
                }

            }
            MessageBox.Show(sb.ToString());
        }
        /// <summary>
        /// Shows/Hides the needed labels.
        /// </summary>
        /// <param name="toHide"><c>true</c> if they should be hided otherwise <c>false</c></param>
        private void hideScoreLabels(bool toHide)
        {
            if (toHide)
            {
                playerScoreLabel.Visibility = Visibility.Hidden;
                dealerScoreLabel.Visibility = Visibility.Hidden;
                statusTextLabel.Visibility = Visibility.Hidden;
                historyTextLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                playerScoreLabel.Visibility = Visibility.Visible;
                dealerScoreLabel.Visibility = Visibility.Visible;
                statusTextLabel.Visibility = Visibility.Visible;
                historyTextLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
