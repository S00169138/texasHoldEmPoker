using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    public class Discards
    {
        private Card[] cards = new Card[52];
        private int _index = 0;
        private Deck _deck;

        public Discards()
        {
        }

        public void setDeck(Deck deck)
        {
            _deck = deck;
        }

        public void addCard(Card card)
        {
            cards[_index] = card;
            _index++;
        }

        public void addCard()
        {
            cards[_index] = _deck.drawCard();
            _index++;
        }

        public Card[] takeDiscards()
        {

            Card[] discs = new Card[_index];
            int i = 0;
            foreach ( Card c in cards )
            {
                if ( c != null )
                {
                    discs[i] = c;
                    i++;
                    cards[_index - 1] = null;
                    _index--;
                }
            }
            return discs;
        }
    }
}
