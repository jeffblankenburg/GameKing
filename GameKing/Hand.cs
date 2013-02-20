using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameKing
{
    public class Hand
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

        public string Check(string GameType)
        {
            Sort();

            if (GameType == "DEUCESWILD")
            {
                if (IsRoyalFlush()) return "ROYALFLUSHNODEUCES";
                if (DEUCES_IsFourDeuces()) return "FOURDEUCES";
                if (DEUCES_IsRoyalFlushWithDeuces()) return "ROYALFLUSHWITHDEUCES";
                if (DEUCES_IsFiveOfAKind()) return "FIVEOFAKIND";
                //if (DEUCES_IsStraightFlush()) return "STRAIGHTFLUSH";
                if (DEUCES_IsFourOfAKind()) return "FOUROFAKIND";
                if (DEUCES_IsFullHouse()) return "FULLHOUSE";
                if (DEUCES_IsFlush()) return "FLUSH";
                if (DEUCES_IsStraight()) return "STRAIGHT";
                if (DEUCES_IsThreeOfAKind()) return "THREEOFAKIND";
            }
            else
            {
                if (IsRoyalFlush()) return "ROYALFLUSH";
                if (IsStraightFlush()) return "STRAIGHTFLUSH";
                if (IsFourOfAKind()) return "FOUROFAKIND";
                if (IsFullHouse()) return "FULLHOUSE";
                if (IsFlush()) return "FLUSH";
                if (IsStraight()) return "STRAIGHT";
                if (IsThreeOfAKind()) return "THREEOFAKIND";
                if (IsTwoPair()) return "TWOPAIR";
                if (IsJacksOrBetter()) return "JACKSORBETTER";
                if (IsPair()) return "PAIR";
            }
            return "NOTHING";
        }

        private int CountDeuces()
        {
            int totalDeuces = (from s in SortedCards where s.Value.Number == 2 select s).Count();
            return totalDeuces;
        }

        private bool IsRoyalFlush()
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
            if ((SortedCards[1].Value.Number == SortedCards[0].Value.Number - 1) && (SortedCards[2].Value.Number == SortedCards[1].Value.Number - 1) && (SortedCards[3].Value.Number == SortedCards[2].Value.Number - 1) && (SortedCards[4].Value.Number == SortedCards[3].Value.Number - 1)) return true;
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
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number) && (SortedCards[0].Value.Number >= 11)) return true;
            if ((SortedCards[1].Value.Number == SortedCards[2].Value.Number) && (SortedCards[1].Value.Number >= 11)) return true;
            if ((SortedCards[2].Value.Number == SortedCards[3].Value.Number) && (SortedCards[2].Value.Number >= 11)) return true;
            if ((SortedCards[3].Value.Number == SortedCards[4].Value.Number) && (SortedCards[3].Value.Number >= 11)) return true;
            return false;
        }

        public bool IsPair()
        {
            if (SortedCards[0].Value.Number == SortedCards[1].Value.Number) return true;
            if (SortedCards[1].Value.Number == SortedCards[2].Value.Number) return true;
            if (SortedCards[2].Value.Number == SortedCards[3].Value.Number) return true;
            if (SortedCards[3].Value.Number == SortedCards[4].Value.Number) return true;
            return false;
        }

        public bool DEUCES_IsPair()
        {
            if (SortedCards[0].Value.Number == SortedCards[1].Value.Number) return true;
            if (SortedCards[1].Value.Number == SortedCards[2].Value.Number) return true;
            if (SortedCards[2].Value.Number == SortedCards[3].Value.Number) return true;
            return false;
        }

        private bool DEUCES_IsTwoPair()
        {
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number)) return true;
            return false;
        }

        private bool DEUCES_IsRoyalFlushWithDeuces()
        {
            if ((DEUCES_IsFlush()) && (DEUCES_IsStraight()) && (((from x in SortedCards where x.Value.Number <= 9 where x.Value.Number >= 3 select x).Count()) == 0)) return true;
            return false;
        }

        private bool DEUCES_IsFourDeuces()
        {
            if ((IsFourOfAKind()) && (SortedCards[1].Value.Number == 2)) return true;
            return false;
        }

        private bool DEUCES_IsFiveOfAKind()
        {
            if ((IsFourOfAKind()) && (SortedCards[4].Value.Number == 2)) return true;
            return false;
        }

        private bool DEUCES_IsThreeOfAKind()
        {
            if (CountDeuces() == 2) return true;
            if ((CountDeuces() == 1) & (IsPair())) return true;
            if (IsThreeOfAKind()) return true;
            return false;
        }

        private bool DEUCES_IsStraight()
        {
            if (IsStraight()) return true;
            if (CountDeuces() == 1)
            {
                if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 1) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 1) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number + 1)) return true;
                if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 2) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 1) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number + 1)) return true;
                if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 1) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 2) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number + 1)) return true;
                if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 1) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 1) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number + 2)) return true;
            }
            if (CountDeuces() == 2)
            {
               if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 1) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 1)) return true;
               if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 2) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 1)) return true;
               if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 1) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 2)) return true;
               if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 3) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 1)) return true;
               if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 2) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 2)) return true;
               if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 1) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 3)) return true;
            }
            if (CountDeuces() == 3)
            {
                if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 1)) return true;
                if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 2)) return true;
                if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 3)) return true;
                if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 4)) return true;
            }
            return false;
        }

        private bool DEUCES_IsFlush()
        {
            if (IsFlush()) return true;
            if (CountDeuces() == 1)
            {
                if ((SortedCards[0].Suit.ID == SortedCards[1].Suit.ID) && (SortedCards[1].Suit.ID == SortedCards[2].Suit.ID) && (SortedCards[2].Suit.ID == SortedCards[3].Suit.ID)) return true;
            }
            if (CountDeuces() == 2)
            {
                if ((SortedCards[0].Suit.ID == SortedCards[1].Suit.ID) && (SortedCards[1].Suit.ID == SortedCards[2].Suit.ID)) return true;
            }
            if (CountDeuces() == 3)
            {
                if ((SortedCards[0].Suit.ID == SortedCards[1].Suit.ID)) return true;
            }
            return false;
        }

        private bool DEUCES_IsFullHouse()
        {
            if (IsFullHouse()) return true;
            if (CountDeuces() == 1)
            {
                if (DEUCES_IsTwoPair()) return true;
            }
            if (CountDeuces() == 2)
            {
                if (DEUCES_IsPair()) return true;
            }
            if (CountDeuces() == 3) return true;
            return false;
        }

        private bool DEUCES_IsFourOfAKind()
        {
            if (IsFourOfAKind()) return true;
            if (CountDeuces() == 1)
            {
                if (IsThreeOfAKind()) return true;
            }
            if (CountDeuces() == 2)
            {
                if (DEUCES_IsPair()) return true;
            }
            if (CountDeuces() == 3) return true;
            return false;
        }
    }
}
