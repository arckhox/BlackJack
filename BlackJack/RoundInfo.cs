using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class RoundInfo
    {
        private int roundNumber = 0;
        private int betAmount = 0;
        private int playerScore = 0;
        private int dealerScore = 0;
        private bool playerWon = false;
        public RoundInfo(int roundNumber, int betAmount, int playerScore, int dealerScore, bool playerWon) 
        { 
            this.roundNumber = roundNumber;
            this.betAmount = betAmount;
            this.playerScore = playerScore;
            this.dealerScore = dealerScore;
            this.playerWon = playerWon;
        }
        public string getString()
        {
            string stringToReturn = "Round " + this.roundNumber;
            if (playerWon)
            {
                stringToReturn += " -> You Won " + betAmount + " ";
            }
            else
            {
                stringToReturn += " -> You Lost " + betAmount + " ";
            }
            stringToReturn += "With Player " + this.playerScore + " And Dealer " + this.dealerScore;
            return stringToReturn;
        }
    }
}
