//Datum: Check Github, for commits and pushes
//Auteur: Arsalan Khosrojerdi
//Discription: Deck Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BlackJack
{
    ///<summary>
    /// The Deck Class represents the deck used for the current game, it can contain more than 1 deck in it(shown by the deckCount attribute)
    /// it also contains a list of the cards in the deck
    ///</summary>
    internal class Deck
    {
        ///<summary>
        /// Count of the decks used 
        ///</summary>
        private int deckCount;
        ///<summary>
        /// A list that contains all the cards
        ///</summary>
        private List<Card> cards;
        /// <summary>
        /// Default Constructor for the Deck Class.
        /// Creates the deck and shuffles the cards.
        /// </summary>
        /// <param name="deckCount">An integer that represents the number of the decks</param>
        public Deck(int deckCount) // Main Constructor
        {
            cards = new List<Card>();
            this.deckCount = deckCount;
            CreateDeck(deckCount); // Creates the cards and adds them to the "cards" list
            ShuffleCards(500); // swap cards randomly for 500 times in the deck
        }
        /// <summary>
        /// Creates the deck
        /// </summary>
        /// <param name="deckCount">An integer that represents the number of the decks</param>
        private void CreateDeck(int deckCount)
        {
            while (cards.Count < (deckCount * 52))
            {
                CreateCardsPerSuit("Clubs", true);
                CreateCardsPerSuit("Spades", true);
                CreateCardsPerSuit("Diamonds", false);
                CreateCardsPerSuit("Hearts", false);
            }
        }
        /// <summary>
        /// It calculates the special cards number. for example J is 11,Q is 12 and...
        /// </summary>
        /// <param name="number">An integer that represents the index of the card being created</param>
        private string CalculateCardNumber(int number)
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
        /// <summary>
        /// It creates cards differentiated by each suit(13 of each suit)
        /// </summary>
        /// <param name="suit">Suit of the card</param>
        /// <param name="isSuitColorBlack"><c>true</c> if the color of the suit is black, otherwise <c>false</c></param>
        private void CreateCardsPerSuit(string suit, bool isSuitColorBlack)
        {
            for (int i = 1; i < 14; i++) // Clubs
            {
                string cardNumber = CalculateCardNumber(i); // This function Calculates the Character of the card(J,Q,K,A,2,3,4 and ...)
                Card tempCard = new Card(cardNumber, suit, isSuitColorBlack, i >= 11 ? 10 : i); //First Argument is the card number/character(K,A,5,4 and ...),2nd arg is the suit(Clubs,Hearts or..) ,3rd is the color(true if black), 4th is the value of the card
                cards.Add(tempCard);
            }
        }
        /// <summary>
        /// Swaps the location of 2 cards in the deck (Used mostly for shuffling the deck)
        /// </summary>
        /// <param name="firstCardIndex">An integer that represents the index of the <c>first</c> card being swapped</param>
        /// <param name="secondCardIndex">An integer that represents the index of the <c>Second</c> card being swapped</param>
        private void SwapCards(int firstCardIndex ,int secondCardIndex)
        {
            Card tempCard = cards[firstCardIndex];
            cards[firstCardIndex] = cards[secondCardIndex];
            cards[secondCardIndex] = tempCard;
        }
        /// <summary>
        /// Shuffles the cards. swaps the location of random cards in the deck for "<c>shuffleCount</c>" times
        /// </summary>
        /// <param name="shuffleCount">The times you need to swap the locations of 2 random cards</param>
        public void ShuffleCards(int shuffleCount) // shuffles cards randomly
        {
            Random random = new Random();
            for (int i = 0; i < shuffleCount; i++) // Swaps the location of 2 cards on the list for "shuffleCount" times
            {
                SwapCards(random.Next(0, 51), random.Next(0, 52));
            }
        }
        /// <summary>
        /// picks(returns) the first card on the deck and removes it from the list
        /// </summary>
        /// <returns>the first card on the deck</returns>
        public Card PickCard()
        {
            Card cardToReturn = cards[0];
            cards.RemoveAt(0);
            return cardToReturn;
        }
        /// <summary>
        /// getter for the <c>deckCount</c> attribute of the Deck Class
        /// </summary>
        /// <returns>deckCount</returns>
        public int GetDeckCount()
        {
            return deckCount;
        }
        /// <summary>
        /// Returns the number of remaining cards in the deck
        /// </summary>
        /// <returns> The count of remaining cards in the deck</returns>
        public int GetCardsCount()
        {
            return cards.Count();
        }
    }
}
