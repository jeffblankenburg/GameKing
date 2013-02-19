using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameKing
{
    class VideoPokerGame
    {
        public Deck Deck;
        public Hand Hand;
        public List<string> WinningHands;
        public List<int> PayTable1;
        public List<int> PayTable2;
        public List<int> PayTable3;
        public List<int> PayTable4;
        public List<int> PayTable5;

        public VideoPokerGame(string GameType)
        {
            LoadPayTable(GameType);
            Deck = new Deck(GameType);
            Hand = new Hand();
            Deal();
        }

        public void Deal()
        {
            for (int i = 0; i <= 4; i++)
            {
                Hand.Cards.Add(Deck.Draw());
                Hand.Held.Add(false);
            }
        }

        public void Draw()
        {
            for (int i = 0; i <= 4; i++)
            {
                if (!Hand.Held[i])
                {
                    Hand.Cards[i] = Deck.Draw();
                }
            }
        }

        public void LoadPayTable(string GameType)
        {
            switch (GameType)
            {
                case "JACKSORBETTER":
                    WinningHands = new List<string> { "ROYAL FLUSH", "STRAIGHT FLUSH", "4 OF A KIND", "FULL HOUSE", "FLUSH", "STRAIGHT", "3 OF A KIND", "TWO PAIR", "JACKS OR BETTER" };
                    PayTable1 = new List<int> { 250, 50, 25, 9, 5, 4, 3, 2, 1 };
                    PayTable2 = new List<int> { 500, 100, 50, 18, 10, 8, 6, 4, 2 };
                    PayTable3 = new List<int> { 750, 150, 75, 27, 15, 12, 9, 6, 3 };
                    PayTable4 = new List<int> { 1000, 200, 100, 36, 20, 16, 12, 8, 4 };
                    PayTable5 = new List<int> { 4000, 250, 125, 45, 25, 20, 15, 10, 5 };
                    break;
                case "DEUCESWILD":
                    WinningHands = new List<string> { "ROYAL FLUSH NO 2s", "4 DEUCES", "ROYAL FLUSH WITH 2s", "5 OF A KIND", "STRAIGHT FLUSH", "4 OF A KIND", "FULL HOUSE", "FLUSH", "STRAIGHT", "3 OF A KIND" };
                    PayTable1 = new List<int> { 250, 200, 20, 12, 9, 5, 3, 2, 2, 1 };
                    PayTable2 = new List<int> { 500, 400, 40, 24, 18, 10, 6, 4, 4, 2 };
                    PayTable3 = new List<int> { 750, 600, 60, 36, 27, 15, 9, 6, 6, 3 };
                    PayTable4 = new List<int> { 1000, 800, 80, 48, 36, 20, 12, 8, 8, 4 };
                    PayTable5 = new List<int> { 4000, 1000, 100, 60, 45, 25, 15, 10, 10, 5 };
                    break;
            }
        }

        public string CheckHand()
        {
            return Hand.Check();
        }
    }
}
