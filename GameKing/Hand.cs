using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameKing
{
    class Hand
    {
        public List<Card> Cards = new List<Card>();
        public List<Card> SortedCards = new List<Card>();
        public List<bool> Held = new List<bool>();

        public void Hold(int card, bool state)
        {
            Held[card] = state;
        }

        public void Sort()
        {
            SortedCards.Clear();
            var cards = Cards.OrderByDescending(x => x.Value.Number);
            foreach (Card c in cards)
            {
                SortedCards.Add(c);
            }
        }

        public string Check()
        {
            Sort();

            if (IsRoyalFlush()) return "ROYALFLUSH";
            if (IsRoyalFlushNoDeuces()) return "ROYALFLUSHNODEUCES";
            if (IsStraightFlush()) return "STRAIGHTFLUSH";
            if (IsFourOfAKind()) return "FOUROFAKIND";
            if (IsFullHouse()) return "FULLHOUSE";
            if (IsFlush()) return "FLUSH";
            if (IsStraight()) return "STRAIGHT";
            if (IsThreeOfAKind()) return "THREEOFAKIND";
            if (IsTwoPair()) return "TWOPAIR";
            if (IsJacksOrBetter()) return "JACKSORBETTER";
            return "NOTHING";
        }

        private bool IsRoyalFlush()
        {
            if (IsStraight() && IsFlush() && (SortedCards[0].Value.Number == 14) && (SortedCards[1].Value.Number == 13) && (SortedCards[2].Value.Number == 12) && (SortedCards[3].Value.Number == 11) && (SortedCards[4].Value.Number == 10)) return true;
            return false;
        }

        private bool IsRoyalFlushNoDeuces()
        {
            if (IsStraight() && IsFlush() && (SortedCards[0].Value.Number == 14) && (SortedCards[1].Value.Number == 13) && (SortedCards[2].Value.Number == 12) && (SortedCards[3].Value.Number == 11) && (SortedCards[4].Value.Number == 10)) return true;
            return false;
        }

        private bool IsStraightFlush()
        {
            if (IsStraight() && IsFlush()) return true;
            return false;
        }

        private bool IsFourOfAKind()
        {
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number)) return true;
            if ((SortedCards[1].Value.Number == SortedCards[2].Value.Number) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number) && (SortedCards[3].Value.Number == SortedCards[4].Value.Number)) return true;
            return false;
        }

        private bool IsFullHouse()
        {
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number) && (SortedCards[3].Value.Number == SortedCards[4].Value.Number)) return true;
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number) && (SortedCards[3].Value.Number == SortedCards[4].Value.Number)) return true;
            return false;
        }

        private bool IsFlush()
        {
            if ((SortedCards[0].Suit.ID == SortedCards[1].Suit.ID) && (SortedCards[1].Suit.ID == SortedCards[2].Suit.ID) && (SortedCards[2].Suit.ID == SortedCards[3].Suit.ID) && (SortedCards[3].Suit.ID == SortedCards[4].Suit.ID)) return true;
            return false;
        }

        private bool IsStraight()
        {
            if ((SortedCards[0].Value.Number == SortedCards[0].Value.Number + 1) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 1) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number + 1) && (SortedCards[3].Value.Number == SortedCards[4].Value.Number + 1)) return true;
            if ((SortedCards[0].Value.Number == 14) && (SortedCards[1].Value.Number == 5) && (SortedCards[2].Value.Number == 4) && (SortedCards[3].Value.Number == 3) && (SortedCards[4].Value.Number == 2)) return true;
            return false;
        }

        private bool IsThreeOfAKind()
        {
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number)) return true;
            if ((SortedCards[1].Value.Number == SortedCards[2].Value.Number) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number)) return true;
            if ((SortedCards[2].Value.Number == SortedCards[3].Value.Number) && (SortedCards[3].Value.Number == SortedCards[4].Value.Number)) return true;
            return false;
        }

        private bool IsTwoPair()
        {
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number)) return true;
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number) && (SortedCards[3].Value.Number == SortedCards[4].Value.Number)) return true;
            if ((SortedCards[1].Value.Number == SortedCards[2].Value.Number) && (SortedCards[3].Value.Number == SortedCards[4].Value.Number)) return true;
            return false;
        }

        private bool IsJacksOrBetter()
        {

            return false;
        }
    }
}
