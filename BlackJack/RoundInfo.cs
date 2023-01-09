//Datum: Check Github, for commits and pushes
//Auteur: Arsalan Khosrojerdi
//Discription: RoundInfo Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    ///<summary>
    /// The RoundInfo Class Contains the info about the previous round. Player and dealers score(int), players bet amount(int) and the result of the round(won,lost or pushed)
    ///</summary>
    class RoundInfo
    {
        ///<summary>
        /// Round number(int) contained in the RoundInfo Class
        ///</summary>
        private int roundNumber = 0;
        ///<summary>
        /// The players bet amount(int) contained in the RoundInfo Class
        ///</summary>
        private int betAmount = 0;
        ///<summary>
        /// The players score(int) contained in the RoundInfo Class
        ///</summary>
        private int playerScore = 0;
        ///<summary>
        /// The dealers score(int) contained in the RoundInfo Class
        ///</summary>
        private int dealerScore = 0;
        ///<summary>
        /// <c>true</c> if the player won the round otherwise <c>false</c>
        ///</summary>
        private bool playerWon = false;
        /// <summary>
        /// Main Constructor for the RoundInfo Class. It sets the values of all the attributes contained in the RoundInfo Class
        /// </summary>
        /// <param name="roundNumber">Round number(int)</param>
        /// <param name="betAmount">The players bet amount(int)</param>
        /// <param name="playerScore">The players score(int) </param>
        /// <param name="dealerScore">The dealers score(int)</param>
        /// <param name="playerWon"><c>true</c> if the player won the round otherwise <c>false</c></param>
        public RoundInfo(int roundNumber, int betAmount, int playerScore, int dealerScore, bool playerWon) 
        { 
            this.roundNumber = roundNumber;
            this.betAmount = betAmount;
            this.playerScore = playerScore;
            this.dealerScore = dealerScore;
            this.playerWon = playerWon;
        }
        /// <summary>
        /// Makes a formatted string to show the user later in the UI. contains information about the previos round.
        /// </summary>
        /// <returns>The formatted string</returns>
        public string GetString()
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
