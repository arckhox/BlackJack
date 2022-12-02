using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Game
    {
        private Deck deck;
        private const int START_BALANCE = 100;
        private int balance;
        private int playerScore;
        private int dealerScore;
        private List<Card> playersCards;
        private List<Card> dealersCards;

        public Game(int deckCount) // main constructor
        {
            deck = new Deck(deckCount);
            playerScore = 0;
            dealerScore = 0;
            playersCards = new List<Card>();
            dealersCards = new List<Card>();
            this.balance = START_BALANCE;
            
        }

        public void initiateGame()
        {
            resetData();
        }

        public string pickCardForPlayer()
        {
            Card tempCard = deck.pickCard();
            if (tempCard.getNumber() == "A") // Aces can be 11 or 1
            {
                if (playerScore + 11 <= 21)
                {
                    playerScore += 11;
                }
            }
            else
            {
                playerScore += tempCard.getValue();
            }
            playersCards.Add(tempCard);
            return (tempCard.getSuit() + " " + tempCard.getNumber());
        }
        public string pickCardForDealer()
        {
            Card tempCard = deck.pickCard();
            dealerScore += tempCard.getValue();
            dealersCards.Add(tempCard);
            return (tempCard.getSuit() + " " + tempCard.getNumber());
        }
        public int getPlayerScore()
        {
            return playerScore;
        }
        public int getDealerScore()
        {
            return dealerScore;
        }
        public int getBalance()
        {
            return this.balance;
        }
        public string getLastDealerCardNumber()
        {
            return this.dealersCards.Last().getNumber();
        }
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
        public string getLastPlayerCardNumber()
        {
            return this.playersCards.Last().getNumber();
        }
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
        public void resetBalance()
        {
            this.balance = START_BALANCE;
        }
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
