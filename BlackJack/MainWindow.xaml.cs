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

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game blackJack;
        public MainWindow()
        {
            InitializeComponent();
            int deckCount = 1;
            blackJack = new Game(deckCount);
            blackJack.initiateGame();
        }

        private void dealButton_Click(object sender, RoutedEventArgs e)
        {
            blackJack.changeBalance(Convert.ToInt32(calculateBetValue()) * (-1));
            playerBalanceLabel.Content = blackJack.getBalance();
            dealerListView.Items.Add(blackJack.pickCardForDealer());
            playerListView.Items.Add(blackJack.pickCardForPlayer());
            playerListView.Items.Add(blackJack.pickCardForPlayer());
            playerScoreLabel.Content = blackJack.getPlayerScore();
            playerBalanceLabel.Content = blackJack.getBalance();
            dealerScoreLabel.Content = blackJack.getDealerScore();
            dealButton.IsEnabled = false;
            hitButton.IsEnabled = true;
            standButton.IsEnabled = true;
            playerBetSlider.IsEnabled = false;
            statusTextLabel.Background = Brushes.Black;
            statusTextLabel.Content = "Hit Or Stand? ;)";

        }

        private void hitButton_Click(object sender, RoutedEventArgs e)
        {
            playerListView.Items.Add(blackJack.pickCardForPlayer());
            playerScoreLabel.Content = blackJack.getPlayerScore();
            if (blackJack.getPlayerScore() > 21)
            {
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                gameLost();
            }
        }

        private void standButton_Click(object sender, RoutedEventArgs e)
        {
            while (blackJack.getDealerScore() < 16)
            {
                dealerListView.Items.Add(blackJack.pickCardForDealer());
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
                    blackJack.changeBalance(Convert.ToInt32(playerBetLabel.Content));
                    restartGame();
                    return;
                }
                gameWon();
            }
        }
        private void gameLost()
        {
            restartGame();
            if (blackJack.getBalance() == 0)
            {
                playerBetSlider.IsEnabled = false;
                statusTextLabel.Content = "Game Lost! Start a new game to continue";
            }
            else
            {
                statusTextLabel.Content = "Round Lost!";
            }
            statusTextLabel.Background = Brushes.DarkRed;
            playerBetSlider.Value = 0;
        }
        private void gameWon()
        {
            statusTextLabel.Content = "Round Won!";
            statusTextLabel.Background = Brushes.DarkGreen;
            blackJack.changeBalance((Convert.ToInt32(playerBetLabel.Content)) * 2);
            restartGame();
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

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            playerBetLabel.Content = Math.Round(((playerBetSlider.Value * 0.1) * blackJack.getBalance()),MidpointRounding.ToPositiveInfinity);
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
            restartGame();
            blackJack.resetBalance();
            statusTextLabel.Content = " New Game started Start betting";
            statusTextLabel.Background = Brushes.Black; 
        }
        private double calculateBetValue()
        {
            return Math.Round(((playerBetSlider.Value * 0.1) * blackJack.getBalance()), MidpointRounding.ToPositiveInfinity);
        }
    }
}
