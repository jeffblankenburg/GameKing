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
            }

            return payTable;
        }

        public string CheckHand()
        {
            return Hand.Check();
        }
    }
}
