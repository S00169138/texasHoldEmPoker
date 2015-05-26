using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    public class Hand
    {
        private Card[] cards = new Card[2];
        private int _index = 0;

        public Hand()
        {

        }

        public void addCard(Card card)
        {
            cards[_index] = card;
            _index++;
        }

        public Card getCard(int index)
        {
          return cards[index];
        }

        public Card[] getCards()
        {
            return cards;
        }

        public void printCards()
        {
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
