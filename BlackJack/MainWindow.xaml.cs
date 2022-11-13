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
        }
    }
}
