using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameKing
{
    [DataContract]
    public class Hand
    {
        [DataMember]
        public string Id;
        [DataMember]
        public List<Card> Cards = new List<Card>();
        [DataMember]
        public List<Card> SortedCards = new List<Card>();
        [DataMember]
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
                if (IsRoyalFlush()) return "ROYAL FLUSH NO DEUCES";
                if (DEUCES_IsFourDeuces()) return "4 DEUCES";
                if (DEUCES_IsRoyalFlushWithDeuces()) return "ROYAL FLUSH WITH DEUCES";
                if (DEUCES_IsFiveOfAKind()) return "5 OF A KIND";
                if (DEUCES_IsStraightFlush()) return "STRAIGHT FLUSH";
                if (DEUCES_IsFourOfAKind()) return "4 OF A KIND";
                if (DEUCES_IsFullHouse()) return "FULL HOUSE";
                if (DEUCES_IsFlush()) return "FLUSH";
                if (DEUCES_IsStraight()) return "STRAIGHT";
                if (DEUCES_IsThreeOfAKind()) return "3 OF A KIND";
            }
            else
            {
                if (IsRoyalFlush()) return "ROYAL FLUSH";
                if (IsStraightFlush()) return "STRAIGHT FLUSH";
                if (IsFourOfAKind()) return "4 OF A KIND";
                if (IsFullHouse()) return "FULL HOUSE";
                if (IsFlush()) return "FLUSH";
                if (IsStraight()) return "STRAIGHT";
                if (IsThreeOfAKind()) return "3 OF A KIND";
                if (IsTwoPair()) return "TWO PAIR";
                if (IsJacksOrBetter()) return "JACKS OR BETTER";
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
            if (IsStraight() && IsFlush() && (SortedCards[4].Value.Number == 10)) return true;
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
            if ((SortedCards[0].Value.Number == SortedCards[1].Value.Number + 1) && (SortedCards[1].Value.Number == SortedCards[2].Value.Number + 1) && (SortedCards[2].Value.Number == SortedCards[3].Value.Number + 1) && (SortedCards[3].Value.Number == SortedCards[4].Value.Number + 1)) return true;
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

        private bool DEUCES_IsStraightFlush()
        {
            if (DEUCES_IsStraight() && DEUCES_IsFlush()) return true;
            return false;
        }

    }
}
