using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    public class Table
    {
        private Card[] cards = new Card[5];
        private int _index = 0;
        private int bets = 0;

        public Table()
        {

        }

        public void addToTable(Card c)
        {
            cards[_index] = c;
            _index++;
        }

        public int getBets()
        {
            return bets;
        }

        public void addBets(int amount)
        {
            bets += amount;

        }        

        public void setBets(int amount)
        {
            bets = amount;
        }

        public Card[] getCards()
        {
            return cards;
        }

        public void printTable()
        {
            Console.WriteLine("Cards on the table: ");
            foreach ( Card c in cards )
            {
                if ( c != null )
                {
                    c.printCard();
                }
            }
        }
    }
}
