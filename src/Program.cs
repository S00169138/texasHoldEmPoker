using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    class Program
    {
        static Discards discards = new Discards();
        static Deck deck = new Deck(discards);
        static Table table = new Table();
        static Random rand = new Random();
        static ConsoleColor[] colors = new ConsoleColor[10] { ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.DarkGreen,
                                                            ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow,
                                                            ConsoleColor.White, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.Gray};

        static void Main(string[] args)
        {
            printHeader();
            discards.setDeck(deck);
            deck.setupDeck();

            Console.WriteLine("How many players will be playing?");
            int num = Convert.ToInt32(Console.ReadLine());

            List<Player> players = new List<Player>();
            for ( int i = 0; i < num + 1; i++ )
            {
                players.Add(new Player("Player " + i, new Hand(), colors[rand.Next(colors.Length)]));
            }
            foreach ( Player p in players )
            {
                deck.dealCardsToAPlayer(p);
                p.printPlayersHand();
            }
            discards.addCard();
            for ( int i = 0; i < 5; i++ )
            {
                table.addToTable(deck.drawCard());
            }
            table.printTable();

            foreach ( Player p in players )
            {
                p.setScore(assignScore(p.getPlayersHand()));
            }
            players.Sort(delegate(Player p1, Player p2)
            {
                return p1.getScore().CompareTo(p2.getScore());
            });
            Console.WriteLine();
            foreach ( var p in players )
            {
                Console.WriteLine(p.getName() + ": " + p.getScore());
            }


            Console.Read();
        }

        static int assignScore(Hand hand)
        {
            int highestCard = 0;

            List<Card> allcards = new List<Card>();

            foreach ( Card c in hand.getCards() )
            {
                allcards.Add(c);
                if ( c.getValue() > highestCard )
                {
                    highestCard = c.getValue();
                }
            }

            foreach ( Card c in table.getCards() )
            {
                allcards.Add(c);
            }

            allcards.Sort(delegate(Card card1, Card card2)
            {
                return card1.getValue().CompareTo(card2.getValue());
            });


            //Royal Flush
            var scoreIndentifier = from ac in allcards
                                   where ( ac.getValue() >= 8 && ac.getValue() <= 12 ) && ( ac.getType() == allcards[0].getType() )
                                   select ac;

            if ( scoreIndentifier.Count() == 7 )
            {
                return 9000 + highestCard;
            }

            //Straight Flush
            scoreIndentifier = from ac in allcards
                               where ( ac.getType() == allcards[0].getType() ) && ( ac.getValue() >= allcards[0].getValue() )
                               select ac;

            if ( scoreIndentifier.Count() == 7 && ( allcards[6].getValue() - allcards[0].getValue() == 6 ) )
            {
                return 8000 + allcards[6].getValue() * 10 + highestCard;
            }

            //Four of a Kind
            scoreIndentifier = from ac in allcards
                               where ac.getValue() == allcards[3].getValue()
                               select ac;

            if ( scoreIndentifier.Count() == 4 )
            {
                return 7000 + ( allcards[3].getValue() + 1 ) * 10 + highestCard;
            }

            //Full House
            var scoreIndentifier2 = from ac in allcards
                                    group ac by ac.getValue();
            if ( scoreIndentifier2.Count() == 3 || scoreIndentifier2.Count() == 4 )
            {
                int groupsOf3 = 0, groupsOf2 = 0;
                int high3 = 0, high2 = 0;
                foreach ( var si in scoreIndentifier2 )
                {
                    if ( si.Count() == 3 )
                    {
                        foreach ( var k in si )
                        {
                            if ( k.getValue() > high3 )
                            {
                                high3 = k.getValue();
                            }
                        }
                        groupsOf3++;
                    }
                    if ( si.Count() == 2 )
                    {
                        foreach ( var k in si )
                        {
                            if ( k.getValue() > high2 )
                            {
                                high2 = k.getValue();
                            }
                        }
                        groupsOf2++;
                    }

                }

                if ( groupsOf3 == 1 && groupsOf2 >= 1 )
                {
                    return 6000 + ( high3 > high2 ? high3 : high2 ) * 10 + highestCard;
                }
            }

            //Flush
            scoreIndentifier = from ac in allcards
                               where ( ac.getType() == allcards[0].getType() )
                               select ac;
            if ( scoreIndentifier.Count() == 7 )
            {
                return 5000 + allcards[6].getValue() * 10 + highestCard;
            }

            //Straight
            scoreIndentifier = from ac in allcards
                               where ( ac.getValue() >= allcards[0].getValue() && ac.getValue() <= allcards[6].getValue() )
                               select ac;
            if ( scoreIndentifier.Count() == 7 && ( allcards[6].getValue() - allcards[0].getValue() == 6 ) )
            {
                scoreIndentifier2 = from ac in scoreIndentifier
                                    group ac by ac.getValue();
                if ( scoreIndentifier2.Count() == 7 )
                {
                    return 4000 + allcards[6].getValue() * 10 + highestCard;
                }
            }

            //Three of a Kind
            scoreIndentifier2 = from ac in allcards
                                group ac by ac.getValue();
            if ( scoreIndentifier2.Count() == 5 || scoreIndentifier2.Count() == 3 )
            {
                foreach ( var gr in scoreIndentifier2 )
                {
                    if ( gr.Count() == 3 )
                    {
                        int cValue = 0;
                        foreach ( var t in gr )
                        {
                            if ( t.getValue() > cValue )
                            {
                                cValue = t.getValue();
                            }
                        }
                        return 3000 + ( cValue + 1 ) * 10 + highestCard;
                    }
                }

            }

            //Two pairs
            scoreIndentifier2 = from ac in allcards
                                group ac by ac.getValue();
            int grs = 0;
            int cValue2 = 0;
            foreach ( var gr in scoreIndentifier2 )
            {
                if ( gr.Count() == 2 )
                {
                    foreach ( var t in gr )
                    {
                        if ( t.getValue() > cValue2 )
                        {
                            cValue2 = t.getValue();
                        }
                    }
                    grs++;
                }
            }
            if ( grs == 2 )
            {
                return 2000 + cValue2 * 10 + highestCard;
            }

            //One Pair
            foreach ( Card c in allcards )
            {
                scoreIndentifier = from ac in allcards
                                   where ac.getValue() == c.getValue()
                                   select ac;
                if ( scoreIndentifier.Count() == 2 )
                {
                    return 1000 + ( c.getValue() + 1 ) * 10 + highestCard;
                }
            }

            // High Card

            return highestCard;
        }


        static void printHeader()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\t\t\t\tTexas ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Hold'em ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Poker");
        }
    }
}
