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
using System.Windows.Threading;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game blackJack;
        private DispatcherTimer dispatcherTimer;
        private List<Image> playerCardsImages;
        private List<Image> dealerCardsImages;
        private List<String> dealerCards;
        private List<String> playerCards;
        public MainWindow()
        {
            InitializeComponent();
            playerCardsImages = new List<Image>();
            dealerCardsImages = new List<Image>();
            dealerCards = new List<string>();
            playerCards = new List<string>();
            int deckCount = 1;
            blackJack = new Game(deckCount);
            blackJack.initiateGame();
        }

        private void dealButton_Click(object sender, RoutedEventArgs e)
        {
            int playerBet = Convert.ToInt32(playerBetLabel.Content);
            playerScoreLabel.Visibility = Visibility.Hidden;
            dealerScoreLabel.Visibility = Visibility.Hidden;
            statusTextLabel.Visibility = Visibility.Hidden;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            blackJack.changeBalance(Convert.ToInt32(calculateBetValue()) * (-1));
            playerBalanceLabel.Content = blackJack.getBalance();
            string playerbetValue = playerBet.ToString();
            restartGame();
            pickCard(true); // pass true if the card picked is for the dealer
            //dealerCardsView.Children.add
            pickCard(false);
            pickCard(false);
            playerScoreLabel.Content = blackJack.getPlayerScore();
            playerBalanceLabel.Content = blackJack.getBalance();
            dealerScoreLabel.Content = blackJack.getDealerScore();
            dealButton.IsEnabled = false;
            playerBetLabel.Content = playerbetValue;
            playerBalanceLabel.Content = blackJack.getBalance();
            playerBetSlider.IsEnabled = false;
            statusTextLabel.Background = Brushes.Black;
            statusTextLabel.Content = "Hit Or Stand? ;)";
            if (blackJack.getPlayerScore() == 21)
            {
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                standButton_Click(null, null);
            }
            // check if the player has enough balance to double down the bet(already subtracted from his balance once)
            else if (checkPlayerBalance(playerBet))
            {
                doubleDownButton.IsEnabled = true;
            }
            



        }

        private void hitButton_Click(object sender, RoutedEventArgs e)
        {
            playerScoreLabel.Visibility = Visibility.Hidden;
            dealerScoreLabel.Visibility = Visibility.Hidden;
            statusTextLabel.Visibility = Visibility.Hidden;
            pickCard(false);
            playerScoreLabel.Content = blackJack.getPlayerScore();
            if (blackJack.getPlayerScore() > 21)
            {
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                gameLost();
            }
            else if (blackJack.getPlayerScore() == 21)
            {
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                standButton_Click(null, null);
            }
        }

        private void standButton_Click(object sender, RoutedEventArgs e)
        {
            playerScoreLabel.Visibility = Visibility.Hidden;
            dealerScoreLabel.Visibility = Visibility.Hidden;
            statusTextLabel.Visibility = Visibility.Hidden;
            while (blackJack.getDealerScore() < 16)
            {
                pickCard(true);
            }
            dealerScoreLabel.Content = blackJack.getDealerScore();
            if (blackJack.getDealerScore() > 21)
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
                    statusTextLabel.Content = "Push!";
                    statusTextLabel.Background = Brushes.DarkGray;
                    blackJack.changeBalance(Convert.ToInt32(playerBetLabel.Content));
                    hitButton.IsEnabled = false;
                    standButton.IsEnabled = false;
                    playerBetSlider.IsEnabled = true;
                    playerBalanceLabel.Content = blackJack.getBalance();
                    playerBetLabel.Content = "0";
                    playerBetSlider.Value = 0;
                    return;
                }
                gameWon();
            }
        }
        private void doubleDownButton_Click(object sender, RoutedEventArgs e)
        {
            
            int playerBet = Convert.ToInt32(playerBetLabel.Content.ToString());
            blackJack.changeBalance(playerBet * -1);
            playerBalanceLabel.Content = blackJack.getBalance();
            playerBetLabel.Content = playerBet * 2;
            doubleDownButton.IsEnabled = false;
            hitButton_Click(null, null);
            standButton_Click(null,null);
        }
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
        private void gameLost()
        {
            
            if (blackJack.getBalance() == 0)
            {
                dealButton.IsEnabled = false;
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                playerBetSlider.IsEnabled = false;
                playerBalanceLabel.Content = blackJack.getBalance();
                playerBetLabel.Content = "0";
                playerBetSlider.Value = 0;
                statusTextLabel.Content = "Game Lost! Start a new game to continue";
            }
            else
            {
                statusTextLabel.Content = "Round Lost!";
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                playerBetSlider.IsEnabled = true;
                playerBetLabel.Content = "0";
                playerBetSlider.Value = 0;
            }
            statusTextLabel.Background = Brushes.DarkRed;
        }
        private void gameWon()
        {
            statusTextLabel.Content = "Round Won!";
            statusTextLabel.Background = Brushes.DarkGreen;
            blackJack.changeBalance((Convert.ToInt32(playerBetLabel.Content)) * 2); // als de player wint dan krijgt hij 2x zijn bet terug
            playerBalanceLabel.Content = blackJack.getBalance();
            playerBetLabel.Content = "0";
            playerBetSlider.Value = 0;
            hitButton.IsEnabled = false;
            standButton.IsEnabled = false;
            playerBetSlider.IsEnabled = true;
        }
        private void restartGame()
        {
            blackJack.initiateGame();
            dealButton.IsEnabled = false;
            hitButton.IsEnabled = false;
            standButton.IsEnabled = false;
            playerBetSlider.IsEnabled = true;
            dealerListView.Items.Clear();
            playerListView.Items.Clear();
            statusTextLabel.Content += " Start Betting To Start Next Round";
            playerBalanceLabel.Content = blackJack.getBalance();
            playerBetLabel.Content = "0";
            playerBetSlider.Value = 0;
            playerCardsView.Children.Clear();
            dealerCardsView.Children.Clear();
        }

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

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            blackJack.resetBalance();
            restartGame();
            statusTextLabel.Content = " New Game started Start betting";
            statusTextLabel.Background = Brushes.Black;
            playerScoreLabel.Content = 0;
            dealerScoreLabel.Content = 0;
        }
        private double calculateBetValue()
        {
            return Math.Round(((playerBetSlider.Value * 0.1) * blackJack.getBalance()), MidpointRounding.ToPositiveInfinity); // bet value must be 10% of the players total balance
        }
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
                }
                playerScoreLabel.Visibility = Visibility.Visible;
                dealerScoreLabel.Visibility = Visibility.Visible;
                statusTextLabel.Visibility = Visibility.Visible;
            }

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }


    }
}
