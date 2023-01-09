//Datum: Check Github, for commits and pushes
//Auteur: Arsalan Khosrojerdi
//Discription: Game Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    ///<summary>
    /// The Game Class Contains the Model-Section of the blackjack game. All the information about the player/dealer score, balance and cards are here.
    /// it also contains a list of the cards in the deck
    ///</summary>
    internal class Game
    {
        private Deck deck;
        private const int START_BALANCE = 100;
        private int balance;
        private int playerScore;
        private int dealerScore;
        private List<Card> playersCards;
        private List<Card> dealersCards;

        /// <summary>
        /// Main Constructor for the Game Class.
        /// Initiates the player balance, player&dealer scores and cards list.
        /// </summary>
        /// <param name="deckCount">An integer that represents the number of the decks</param>
        public Game(int deckCount) 
        {
            deck = new Deck(deckCount);
            playerScore = 0;
            dealerScore = 0;
            playersCards = new List<Card>();
            dealersCards = new List<Card>();
            this.balance = START_BALANCE;
            
        }
        /// <summary>
        /// Initiates the game and resets all the data
        /// </summary>
        public void initiateGame()
        {
            resetData();
        }
        /// <summary>
        /// Picks a card(will be remove from the deck afterwards) from the deck and adds it to the playerCards list
        /// </summary>
        /// <returns>A formatted string with the suit of the card and the number</returns>
        public string pickCardForPlayer()
        {
            Card tempCard = deck.pickCard();
            if (tempCard.getNumber() == "A") // Aces can be 11 or 1
            {
                if (playerScore + 11 < 22)
                {
                    playerScore += 11;
                }
                else{
                    playerScore += 1;
                }
            }
            else
            {
                playerScore += tempCard.getValue();
            }
            playersCards.Add(tempCard);
            return (tempCard.getSuit() + " " + tempCard.getNumber());
        }
        /// <summary>
        /// Picks a card(will be remove from the deck afterwards) from the deck and adds it to the dealerCards list
        /// </summary>
        /// <returns>A formatted string with the suit of the card and the number</returns>
        public string pickCardForDealer()
        {
            Card tempCard = deck.pickCard();
            if (tempCard.getNumber() == "A") // Aces can be 11 or 1
            {
                if (dealerScore + 11 < 22)
                {
                    dealerScore += 11;
                }
                else
                {
                    dealerScore += 1;
                }
            }
            else
            {
                dealerScore += tempCard.getValue();
            }
            dealersCards.Add(tempCard);
            return (tempCard.getSuit() + " " + tempCard.getNumber());
        }
        /// <summary>
        ///  Getter for the playerScore attribute of the Game Class
        /// </summary>
        /// <returns>playerScore(int) attribute of the Game Class</returns>
        public int getPlayerScore()
        {
            return playerScore;
        }
        /// <summary>
        ///  Getter for the dealerScore attribute of the Game Class
        /// </summary>
        /// <returns>dealerScore(int) attribute of the Game Class</returns>
        public int getDealerScore()
        {
            return dealerScore;
        }
        /// <summary>
        ///  Getter for the balance attribute of the Game Class
        /// </summary>
        /// <returns>balance(int) attribute of the Game Class</returns>
        public int getBalance()
        {
            return this.balance;
        }
        /// <summary>
        ///  Gets the number of the dealer's last card
        /// </summary>
        /// <returns>the number of the dealer's last card</returns>
        public string getLastDealerCardNumber()
        {
            return this.dealersCards.Last().getNumber();
        }
        /// <summary>
        ///  Gets the suit of the dealer's last card
        /// </summary>
        /// <returns>the suit of the dealer's last card as an Character(the type is a string) (H,C,S or D)</returns>
        public string getLastDealerCardSuit()
        {
            switch (this.dealersCards.Last().getSuit())
            {
                case "Hearts":
                    return "H";
                case "Clubs":
                    return "C";
                case "Spades":
                    return "S";
                case "Diamonds":
                    return "D";
                default:
                    return "?";
            }
        }
        /// <summary>
        ///  Gets the number of the player's last card
        /// </summary>
        /// <returns>the number of the player's last card as an string</returns>
        public string getLastPlayerCardNumber()
        {
            return this.playersCards.Last().getNumber();
        }
        /// <summary>
        ///  Gets the suit of the player's last card
        /// </summary>
        /// <returns>the suit of the player's last card as an Character(the type is a string) (H,C,S or D)</returns>
        public string getLastPlayerCardSuit()
        {
            switch (this.playersCards.Last().getSuit())
            {
                case "Hearts":
                    return "H";
                case "Clubs":
                    return "C";
                case "Spades":
                    return "S";
                case "Diamonds":
                    return "D";
                default:
                    return "?";
            }
        }
        /// <summary>
        ///  Changes the balance of the player if possible
        /// </summary>
        /// <param name="value">The value(Signed int) which the balance of the player should be changed by</param>
        /// <returns><c>true</c> if balance is changed otherwise <c>false</c></returns>
        public bool changeBalance(int value)
        {
            if (balance + value < 0)
            {
                return false;
            }
            else
            {                    
                this.balance += value;
                return true;
            }
        }
        /// <summary>
        ///  Resets the balance of the player
        /// </summary>
        public void resetBalance()
        {
            this.balance = START_BALANCE;
        }
        /// <summary>
        ///  Resets all the data in the current game(As an preperation to start a new one)
        /// </summary>
        private void resetData()
        {
            playerScore = 0;
            dealerScore = 0;
            playersCards.Clear();
            dealersCards.Clear();
            int deckCount = deck.getDeckCount();
            deck = new Deck(deckCount);
        }
    }
}
