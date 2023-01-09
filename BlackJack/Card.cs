//Datum: Check Github, for commits and pushes
//Auteur: Arsalan Khosrojerdi
//Discription: Card Class

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Xps.Serialization;

namespace BlackJack
{
    ///<summary>
    /// The Card Class represents each card which has a number(2,3,4,...,9,10,J,Q,K,A),
    /// a suit(Clubs,Diamonds,...), a value(For example: A = 11 or A = 1)
    /// and a boolean which shows if the card color is black/red
    ///</summary>
    internal class Card
    {
        ///<summary>
        /// card number(2,3,4,...,9,10,J,Q,K,A),
        ///</summary>
        private string number;
        ///<summary>
        /// suit(Clubs,Diamonds,...)
        ///</summary>
        private string suit;
        ///<summary>
        /// a boolean which shows if the card color is black/red
        ///</summary>
        private bool isColorBlack;
        ///<summary>
        /// card value(For example: A = 11 or A = 1)
        ///</summary>
        private int value;

        /// <summary>
        /// Default Constructor for the Card Class
        /// </summary>
        /// <param name="number">A string that represents the number of the card</param>
        /// <param name="suit">
        /// A string that represents the Suit of the card
        /// </param>
        /// <param name="isColorBlack">
        /// A boolean that shows the color of the card, true if black and false if red
        /// </param>
        /// <param name="value">
        /// An integer that represents the value of the card
        /// </param>
        public Card(string number, string suit, bool isColorBlack, int value)
        {
            this.number = number;
            this.suit = suit;
            this.isColorBlack= isColorBlack;
            this.value = value;
        }
        /// <summary>
        /// getter for number attribute of the Card class
        /// </summary>
        /// <returns>Number(string) attribute of the Card class.</returns>
        public string getNumber()
        {
            return number;
        }
        /// <summary>
        /// getter for suit attribute of the Card class
        /// </summary>
        /// <returns>suit(string) attribute of the Card class.</returns>
        public string getSuit()
        {
            return suit;
        }
        /// <summary>
        /// getter for color of the Card 
        /// </summary>
        /// <returns>"Black" or "Red".</returns>
        public string getColor()
        {
            return isColorBlack? "Black" : "Red"; 
        }
        /// <summary>
        /// getter for value attribute of the Card class
        /// </summary>
        /// <returns>value(int)</returns>
        public int getValue()
        {
            return value;
        }
    }
}
