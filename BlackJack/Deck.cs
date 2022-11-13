using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BlackJack
{
    internal class Deck
    {
        private int deckCount;
        private List<Card> cards;
        public Deck(int deckCount) // Main Constructor
        {
            cards = new List<Card>();
            this.deckCount = deckCount;
            createDeck(deckCount); // Creates the cards and adds them to the "cards" list
            shuffleCards(500); // swap cards randomly for 500 times in the deck
        }
        private void createDeck(int deckCount)
        {
            while (cards.Count < (deckCount * 52))
            {
                createCardsPerSuit("Clubs", true);
                createCardsPerSuit("Spades", true);
                createCardsPerSuit("Diamonds", false);
                createCardsPerSuit("Hearts", false);
            }
        }
        private string calculateCardNumber(int number)
        {
            switch (number)
            {
                case 1: return "A";
                case 11: return "J";
                case 12: return "Q";
                case 13: return "K";
                default: return number.ToString();
            }
        }
        private void createCardsPerSuit(string suit, bool isSuitColorBlack)
        {
            for (int i = 1; i < 14; i++) // Clubs
            {
                string cardNumber = calculateCardNumber(i); // This function Calculates the Character of the card(J,Q,K,A,2,3,4 and ...)
                Card tempCard = new Card(cardNumber, suit, isSuitColorBlack, i >= 11 ? 10 : i); //First Argument is the card number/character(K,A,5,4 and ...),2nd arg is the suit(Clubs,Hearts or..) ,3rd is the color(true if black), 4th is the value of the card
                cards.Add(tempCard);
            }
        }
        private void swapCards(int firstCardIndex ,int secondCardIndex) // Swaps the location of 2 cards in the deck
        {
            Card tempCard = cards[firstCardIndex];
            cards[firstCardIndex] = cards[secondCardIndex];
            cards[secondCardIndex] = tempCard;
        }
        public void shuffleCards(int shuffleCount) // shuffles cards randomly
        {
            Random random = new Random();
            for (int i = 0; i < shuffleCount; i++) // Swaps the location of 2 cards on the list for "shuffleCount" times
            {
                swapCards(random.Next(0, 51), random.Next(0, 52));
            }
        }
        public Card pickCard() // picks(returns) the first card on the deck and removes it from the list
        {
            Card cardToReturn = cards[0];
            cards.RemoveAt(0);
            return cardToReturn;
        }
        public int getDeckCount()
        {
            return deckCount;
        }
    }
}
