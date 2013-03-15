using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLogic
{
    public class VideoPokerGame
    {
        public Deck Deck;
        public Hand Hand;
        public List<PayTableItem> PayTable;

        public VideoPokerGame(string GameType)
        {
            PayTable = LoadPayTable(GameType);
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

        private List<PayTableItem> LoadPayTable(string GameType)
        {
            List<PayTableItem> payTable = new List<PayTableItem>();

            switch (GameType)
            {
                case "DEUCESWILD":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH NO DEUCES........", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "4 DEUCES.....................................", Coin1 = 200, Coin2 = 400, Coin3 = 600, Coin4 = 800, Coin5 = 1000 });
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH WITH DEUCES....", Coin1 = 20, Coin2 = 40, Coin3 = 60, Coin4 = 80, Coin5 = 100 });
                    payTable.Add(new PayTableItem { Title = "5 OF A KIND................................", Coin1 = 12, Coin2 = 24, Coin3 = 36, Coin4 = 48, Coin5 = 60 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "JACKSORBETTER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 25, Coin2 = 50, Coin3 = 75, Coin4 = 100, Coin5 = 125 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "BONUSPOKERDELUXE":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 8, Coin2 = 16, Coin3 = 24, Coin4 = 32, Coin5 = 40 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 6, Coin2 = 12, Coin3 = 18, Coin4 = 24, Coin5 = 30 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "DOUBLEBONUSPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 ACES..........................................", Coin1 = 160, Coin2 = 320, Coin3 = 480, Coin4 = 640, Coin5 = 800 });
                    payTable.Add(new PayTableItem { Title = "4 2s, 3s, 4s...................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "4 5s THRU KINGS.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 6, Coin2 = 12, Coin3 = 18, Coin4 = 24, Coin5 = 30 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "TRIPLEBONUSPOKERPLUS":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 100, Coin2 = 200, Coin3 = 300, Coin4 = 400, Coin5 = 500 });
                    payTable.Add(new PayTableItem { Title = "4 ACES..........................................", Coin1 = 240, Coin2 = 480, Coin3 = 720, Coin4 = 960, Coin5 = 1200 });
                    payTable.Add(new PayTableItem { Title = "4 2s, 3s, 4s...................................", Coin1 = 120, Coin2 = 240, Coin3 = 360, Coin4 = 480, Coin5 = 600 });
                    payTable.Add(new PayTableItem { Title = "4 5s THRU KINGS.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 8, Coin2 = 16, Coin3 = 24, Coin4 = 32, Coin5 = 40 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "ROYALACESBONUSPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 100, Coin2 = 200, Coin3 = 300, Coin4 = 400, Coin5 = 500 });
                    payTable.Add(new PayTableItem { Title = "4 ACES..........................................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "4 2s, 3s, 4s...................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "4 5s THRU KINGS.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "WHITEHOTACES":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "4 ACES..........................................", Coin1 = 240, Coin2 = 480, Coin3 = 720, Coin4 = 960, Coin5 = 1200 });
                    payTable.Add(new PayTableItem { Title = "4 2s, 3s, 4s...................................", Coin1 = 120, Coin2 = 240, Coin3 = 360, Coin4 = 480, Coin5 = 600 });
                    payTable.Add(new PayTableItem { Title = "4 5s THRU KINGS.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 8, Coin2 = 16, Coin3 = 24, Coin4 = 32, Coin5 = 40 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "BONUSPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 ACES..........................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "4 2s, 3s, 4s...................................", Coin1 = 40, Coin2 = 80, Coin3 = 160, Coin4 = 240, Coin5 = 320 });
                    payTable.Add(new PayTableItem { Title = "4 5s THRU KINGS.......................", Coin1 = 25, Coin2 = 50, Coin3 = 75, Coin4 = 100, Coin5 = 125 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 8, Coin2 = 16, Coin3 = 24, Coin4 = 32, Coin5 = 40 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "SUPERACESBONUSPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 60, Coin2 = 120, Coin3 = 180, Coin4 = 240, Coin5 = 300 });
                    payTable.Add(new PayTableItem { Title = "4 ACES..........................................", Coin1 = 400, Coin2 = 800, Coin3 = 1200, Coin4 = 1600, Coin5 = 2000 });
                    payTable.Add(new PayTableItem { Title = "4 2s, 3s, 4s...................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "4 5s THRU KINGS.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 7, Coin2 = 14, Coin3 = 21, Coin4 = 28, Coin5 = 35 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "ACESANDFACESPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 ACES..........................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "4 Js, Qs, Ks...................................", Coin1 = 40, Coin2 = 80, Coin3 = 120, Coin4 = 160, Coin5 = 200 });
                    payTable.Add(new PayTableItem { Title = "4 5s THRU 10s.............................", Coin1 = 25, Coin2 = 50, Coin3 = 75, Coin4 = 100, Coin5 = 125 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 7, Coin2 = 14, Coin3 = 21, Coin4 = 28, Coin5 = 35 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "DOUBLEBONUSDEUCESWILD":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH NO DEUCES........", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "4 DEUCES PLUS ACE...................", Coin1 = 400, Coin2 = 800, Coin3 = 1200, Coin4 = 1600, Coin5 = 2000 });
                    payTable.Add(new PayTableItem { Title = "4 DEUCES.....................................", Coin1 = 200, Coin2 = 400, Coin3 = 600, Coin4 = 800, Coin5 = 1000 });
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH WITH DEUCES....", Coin1 = 25, Coin2 = 50, Coin3 = 75, Coin4 = 100, Coin5 = 125 });
                    payTable.Add(new PayTableItem { Title = "5 ACES..........................................", Coin1 = 160, Coin2 = 320, Coin3 = 480, Coin4 = 640, Coin5 = 800 });
                    payTable.Add(new PayTableItem { Title = "5 3s THRU 5s...............................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "5 6s THRU KINGS.......................", Coin1 = 20, Coin2 = 40, Coin3 = 60, Coin4 = 80, Coin5 = 100 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "DEUCESWILDBONUSPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH NO DEUCES........", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "4 DEUCES PLUS ACE...................", Coin1 = 400, Coin2 = 800, Coin3 = 1200, Coin4 = 1600, Coin5 = 2000 });
                    payTable.Add(new PayTableItem { Title = "4 DEUCES.....................................", Coin1 = 200, Coin2 = 400, Coin3 = 600, Coin4 = 800, Coin5 = 1000 });
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH WITH DEUCES....", Coin1 = 25, Coin2 = 50, Coin3 = 75, Coin4 = 100, Coin5 = 125 });
                    payTable.Add(new PayTableItem { Title = "5 ACES.........................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "5 3s THRU 5s...............................", Coin1 = 40, Coin2 = 80, Coin3 = 120, Coin4 = 160, Coin5 = 200 });
                    payTable.Add(new PayTableItem { Title = "5 6s THRU KINGS.......................", Coin1 = 20, Coin2 = 40, Coin3 = 60, Coin4 = 80, Coin5 = 100 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 13, Coin2 = 26, Coin3 = 39, Coin4 = 52, Coin5 = 65 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "JOKERPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH NO WILD............", Coin1 = 400, Coin2 = 800, Coin3 = 1200, Coin4 = 1600, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "5 OF A KIND................................", Coin1 = 200, Coin2 = 400, Coin3 = 600, Coin4 = 800, Coin5 = 1000 });
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH WITH WILD........", Coin1 = 100, Coin2 = 200, Coin3 = 300, Coin4 = 400, Coin5 = 500 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 20, Coin2 = 40, Coin3 = 60, Coin4 = 80, Coin5 = 100 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 10, Coin2 = 20, Coin3 = 30, Coin4 = 40, Coin5 = 50 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 6, Coin2 = 12, Coin3 = 18, Coin4 = 24, Coin5 = 30 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR...................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "KINGS OR BETTER......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "BLACKJACKBONUSPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 400, Coin2 = 800, Coin3 = 1200, Coin4 = 1600, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "4 ACES WITH BLACK JACK........", Coin1 = 400, Coin2 = 800, Coin3 = 1200, Coin4 = 1600, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "4 2s,3s,4s W/ BLACK JACK........", Coin1 = 400, Coin2 = 800, Coin3 = 1200, Coin4 = 1600, Coin5 = 2000 });
                    payTable.Add(new PayTableItem { Title = "4 of a KIND W/ BLACK JACK....", Coin1 = 160, Coin2 = 320, Coin3 = 480, Coin4 = 640, Coin5 = 800 });
                    payTable.Add(new PayTableItem { Title = "4 ACES or JACKS.........................", Coin1 = 160, Coin2 = 320, Coin3 = 480, Coin4 = 640, Coin5 = 800 });
                    payTable.Add(new PayTableItem { Title = "4 2s, 3s, 4s...................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 25, Coin2 = 50, Coin3 = 75, Coin4 = 100, Coin5 = 125 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 7, Coin2 = 14, Coin3 = 21, Coin4 = 28, Coin5 = 35 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR...................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "DOUBLEDOUBLEBONUSPOKER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 ACES WITH ANY 2,3,4............", Coin1 = 400, Coin2 = 800, Coin3 = 1200, Coin4 = 1600, Coin5 = 2000 });
                    payTable.Add(new PayTableItem { Title = "4 2s,3s,4s W/ ACE,2,3,4.............", Coin1 = 160, Coin2 = 320, Coin3 = 480, Coin4 = 640, Coin5 = 800 });
                    payTable.Add(new PayTableItem { Title = "4 ACES.........................................", Coin1 = 160, Coin2 = 320, Coin3 = 480, Coin4 = 640, Coin5 = 800 });
                    payTable.Add(new PayTableItem { Title = "4 2s, 3s, 4s...................................", Coin1 = 80, Coin2 = 160, Coin3 = 240, Coin4 = 320, Coin5 = 400 });
                    payTable.Add(new PayTableItem { Title = "4 5s THRU KINGS.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR...................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
            }

            return payTable;
        }

        public string CheckHand(string GameType)
        {
            return Hand.Check(GameType);
        }
    }
}
