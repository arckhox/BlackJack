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
    internal class Card
    {
        private string number;
        private string suit;
        private bool isColorBlack;
        private int value;

        public Card(string number, string suit, bool isColorBlack, int value)
        {
            this.number = number;
            this.suit = suit;
            this.isColorBlack= isColorBlack;
            this.value = value;
        }

        public string getNumber()
        {
            return number;
        }

        public string getSuit()
        {
            return suit;
        }
        public string getColor()
        {
            return isColorBlack? "Black" : "Red"; 
        }
        public int getValue()
        {
            return value;
        }
    }
}
