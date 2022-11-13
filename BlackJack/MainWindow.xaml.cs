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
            dealerListView.Items.Add(blackJack.pickCardForDealer());
            playerListView.Items.Add(blackJack.pickCardForPlayer());
            playerListView.Items.Add(blackJack.pickCardForPlayer());
            playerScoreLabel.Content = blackJack.getPlayerScore();
            dealerScoreLabel.Content = blackJack.getDealerScore();
            dealButton.IsEnabled = false;
            hitButton.IsEnabled = true;
            standButton.IsEnabled = true;
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
                    restartGame();
                }
                gameWon();
            }
            hitButton.IsEnabled = false;
            standButton.IsEnabled = false;
        }
        private void gameLost()
        {
            statusTextLabel.Content = "Round Lost!";
            restartGame();
        }
        private void gameWon()
        {
            statusTextLabel.Content = "Round Won!";
            restartGame();
        }
        private void restartGame()
        {
            blackJack.initiateGame();
            dealButton.IsEnabled = true;
            hitButton.IsEnabled = false;
            standButton.IsEnabled = false;
            dealerListView.Items.Clear();
            playerListView.Items.Clear();
            statusTextLabel.Content += " Press Deal To Start Next Round";
        }
    }
}
