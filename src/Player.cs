using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    public class Player
    {
        private string _name;
        private Hand _hand;
        private int noOfGames, noOfWins;
        private int money = 50000;
        private ConsoleColor col = ConsoleColor.White;
        private bool isAI = false;
        private int score = 0;

        public Player(string name, Hand hand)
        {
            _name = name;
            _hand = hand;
        }

        public Player(string name, Hand hand, bool AI)
        {
            _name = name;
            _hand = hand;
            isAI = AI;
        }

        public Player(string name, Hand hand, bool AI, ConsoleColor colour)
        {
            _name = name;
            _hand = hand;
            isAI = AI;
            col = colour;
        }

        public Player(string name, Hand hand, ConsoleColor colour)
        {
            _name = name;
            _hand = hand;
            col = colour;
        }

        public Hand getPlayersHand()
        {
            return _hand;
        }

        public string getName()
        {
            return _name;
        }

        public void setScore(int amount)
        {
            score = amount;
        }

        public int getScore()
        {
            return score;
        }

        public int getMoney()
        {
            return money;
        }

        public void setMoney(int amount)
        {
            money = amount;
        }

        public void addMoney(int amount)
        {
            money += amount;
        }

        public void printStats()
        {
            Console.ForegroundColor = col;
            Console.WriteLine(_name + " has played " + noOfGames + " games: " + noOfWins + " WON " + ( noOfGames - noOfWins ) + " LOST");
            Console.WriteLine(_name + " has " + money + "$");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void printPlayersHand()
        {
            Console.ForegroundColor = col;
            Console.WriteLine(_name + "'s cards: ");
            Console.ForegroundColor = ConsoleColor.White;
            _hand.printCards();
            Console.WriteLine("");
        }

        public void drawsCard(Card card)
        {
            _hand.addCard(card);
        }
    }
}
