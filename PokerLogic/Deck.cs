using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLogic
{
    public class Deck
    {
        protected List<Card> Cards;

        public List<Suit> Suits = new List<Suit> { new Suit { Name = "Hearts", ID = 1 }, new Suit { Name = "Diamonds", ID = 2 }, new Suit { Name = "Clubs", ID = 3 }, new Suit { Name = "Spades", ID = 4 } };
        public List<Value> Values = new List<Value> { new Value { Name = "Two", Number = 2 }, new Value { Name = "Three", Number = 3 }, new Value { Name = "Four", Number = 4 }, new Value { Name = "Five", Number = 5 }, new Value { Name = "Six", Number = 6 }, new Value { Name = "Seven", Number = 7 }, new Value { Name = "Eight", Number = 8 }, new Value { Name = "Nine", Number = 9 }, new Value { Name = "Ten", Number = 10 }, new Value { Name = "Jack", Number = 11 }, new Value { Name = "Queen", Number = 12 }, new Value { Name = "King", Number = 13 }, new Value { Name = "Ace", Number = 14 } };

        public Deck(string gameType)
        {
            Cards = new List<Card>();
            string x = String.Empty;
            foreach (Suit s in Suits)
            {
                foreach (Value v in Values)
                {
                    Cards.Add(new Card(s, v));
                }
            }

            Shuffle();
        }

        public Card Draw()
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        public void Shuffle()
        {
            Random r = new Random();
            for (int i = 0; i < Cards.Count; i++)
            {
                int index1 = i;
                int index2 = r.Next(Cards.Count);
                SwapCard(index1, index2);
            }
        }

        private void SwapCard(int index1, int index2)
        {
            Card card = Cards[index1];
            Cards[index1] = Cards[index2];
            Cards[index2] = card;
        }
    }
}
