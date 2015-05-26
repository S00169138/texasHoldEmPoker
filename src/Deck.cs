using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    public class Deck
    {
        private Card[] _deck = new Card[52];
        private int index = 0;
        private Discards discards;

        public Deck(Discards discs)
        {
            discards = discs;
        }

        public void addCard(int value, int type)
        {
            _deck[index] = new Card(value, type);
            index++;
        }

        public void fillDeck()
        {
            for ( int i = 0; i < 13; i++ )
            {
                for ( int j = 0; j < 4; j++ )
                {
                    addCard(i, j);
                }
            }
        }

        public void setupDeck()
        {
            fillDeck();
            shuffleDeck();
        }

        public Card drawCard()
        {
            if ( _deck[0] != null )
            {
                Card output = _deck[0];
                for ( int i = 0; i < index - 1; i++ )
                {
                    if ( _deck[i + 1] != null )
                    {
                        _deck[i] = _deck[i + 1];
                    }
                    if ( i == index - 1 )
                    {
                        _deck[i] = null;
                    }
                }
                index--;
                return output;
            }
            else
            {
                _deck = discards.takeDiscards();
                return drawCard();
            }

        }

        public void shuffleDeck()
        {
            List<Card> list = new List<Card>();
            Random rand = new Random();
            for ( int i = 0; i < index; i++ )
            {
                list.Add(_deck[i]);
            }
            for ( int i = 0; i < 10000; i++ )
            {
                list.Reverse(0, rand.Next(list.Count));
                Card helper = list[list.Count - 1];
                int r = rand.Next(list.Count / 2);
                list[list.Count - 1] = list[r];
                list[r] = helper;
            }
            for ( int i = 0; i < index; i++ )
            {
                _deck[i] = list[i];
            }
        }

        public void dealCardsToAPlayer(Player player)
        {
            player.drawsCard(drawCard());
            player.drawsCard(drawCard());
        }

        public void shuffleDeck(int seed)
        {
            List<Card> list = new List<Card>();
            Random rand = new Random(seed);
            for ( int i = 0; i < index; i++ )
            {
                list.Add(_deck[i]);
            }
            for ( int i = 0; i < 10000; i++ )
            {
                list.Reverse(0, rand.Next(list.Count));
                Card helper = list[list.Count - 1];
                int r = rand.Next(list.Count / 2);
                list[list.Count - 1] = list[r];
                list[r] = helper;

            }
            for ( int i = 0; i < list.Count; i++ )
            {
                _deck[i] = list[i];
            }
        }

        public void shuffleDeck(int seed, int noOfShuffles)
        {
            List<Card> list = new List<Card>();
            Random rand = new Random(seed);
            for ( int i = 0; i < index; i++ )
            {
                list.Add(_deck[i]);
            }
            for ( int i = 0; i <= noOfShuffles; i++ )
            {
                list.Reverse(0, rand.Next(list.Count));
                Card helper = list[list.Count - 1];
                int r = rand.Next(list.Count / 2);
                list[list.Count - 1] = list[r];
                list[r] = helper;                
            }
            for ( int i = 0; i < index; i++ )
            {
                _deck[i] = list[i];
            }
        }

        public int getDeckSize()
        {
            return index;
        }
    }
}
