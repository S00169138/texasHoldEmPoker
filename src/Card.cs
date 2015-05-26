using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    public class Card
    {
        private int _value;
        private int _type;
        private string valueString = "23456789XJQKA";
        private string typeString = "♠♣♥♦";

        public Card(int value, int type)
        {
            _value = value;
            _type = type;
        }

        public string getCardValue()
        {
            return "" + valueString[_value] + typeString[_type];
        }

        public int getValue()
        {
            return _value;
        }

        public int getType()
        {
            return _type;
        }

        public void printCard()
        {
            Console.ForegroundColor = _type < 2 ? ConsoleColor.White : ConsoleColor.Red;
            Console.Write("" + valueString[_value] + typeString[_type] + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
