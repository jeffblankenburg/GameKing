using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLogic
{
    public class HelpContent
    {
        public string Title { get; set; }
        public List<HelpItem> HelpItems { get; set; }

        public HelpContent()
        {
            
        }

        public HelpContent(string GameType)
        {
            HelpItems = LoadHelp(GameType);
        }

        private List<HelpItem> LoadHelp(string GameType)
        {
            List<HelpItem> templist = new List<HelpItem>();
            
            switch (GameType)
            {
                case "DEUCESWILD":
                    Title = "DEUCES WILD";
                    templist.Add(new HelpItem("FOUR DEUCES", true));
                    templist.Add(new HelpItem("Always keep four deuces.  Always.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("THREE DEUCES", true));
                    templist.Add(new HelpItem("Keep a royal flush, otherwise keep only the three 2s.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("TWO DEUCES", true));
                    templist.Add(new HelpItem("Keep any four of a kind or higher.", false));
                    templist.Add(new HelpItem("Keep four to a royal flush.", false));
                    templist.Add(new HelpItem("Keep four to a straight flush with 2 consecutive cards, 6-7 or higher.", false));
                    templist.Add(new HelpItem("Otherwise just keep the two 2s.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("ONE DEUCE", true));
                    templist.Add(new HelpItem("Keep any four of a kind.", false));
                    templist.Add(new HelpItem("Keep four to a royal flush.", false));
                    templist.Add(new HelpItem("Full house.", false));
                    templist.Add(new HelpItem("Keep four to a straight flush with 3 consecutive cards, 5-7 or higher.", false));
                    templist.Add(new HelpItem("Keep three of a kind.", false));
                    templist.Add(new HelpItem("Keep a straight.", false));
                    templist.Add(new HelpItem("Keep a flush.", false));
                    templist.Add(new HelpItem("Keep four to any straight flush.", false));
                    templist.Add(new HelpItem("Keep three to a royal flush.", false));
                    templist.Add(new HelpItem("Keep three to a straight flush with 2 consecutive cards, 6-7 or higher.", false));
                    templist.Add(new HelpItem("Keep just the 2.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("ZERO DEUCES", true));
                    templist.Add(new HelpItem("Four or five cards to a royal flush.", false));
                    templist.Add(new HelpItem("Keep any straight flush - three of a kind.", false));
                    templist.Add(new HelpItem("Keep four to a straight flush.", false));
                    templist.Add(new HelpItem("Keep 3 to a royal flush.", false));
                    templist.Add(new HelpItem("Keep a pair.", false));
                    templist.Add(new HelpItem("Keep four to a flush.", false));
                    templist.Add(new HelpItem("Keep four to an outside straight.", false));
                    templist.Add(new HelpItem("Keep three to a straight flush.", false));
                    templist.Add(new HelpItem("Keep four to an inside straight, except when you a missing a deuce.", false));
                    templist.Add(new HelpItem("Keep two to a royal flush.", false));
                    templist.Add(new HelpItem("Keep none of the cards.  Get five new ones.", false));
                    break;
                case "ACESANDFACESPOKER":
                    Title = "ACES AND FACES POKER";
                    templist.Add(new HelpItem("A lower payout is set for pairs, straights and flushes with higher payouts given to 4 of a kind combinations so you should look toward any 4 of a kind but the Aces and Faces carry the most payout.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("If you aren't dealt a paying hand, check the list of ranked possible combinations listed below and use the one that will produce the highest result.", true));
                    templist.Add(new HelpItem("Four of a kind, straight flush, royal flush", false));
                    templist.Add(new HelpItem("Four cards to a royal flush", false));
                    templist.Add(new HelpItem("Three of a kind, straight, flush, full house", false));
                    templist.Add(new HelpItem("Four cards to a straight flush", false));
                    templist.Add(new HelpItem("Two pair", false));
                    templist.Add(new HelpItem("Pair of Jacks or better", false));
                    templist.Add(new HelpItem("Three cards to a royal flush", false));
                    templist.Add(new HelpItem("Four cards to a flush", false));
                    templist.Add(new HelpItem("Low pair", false));
                    templist.Add(new HelpItem("Four cards to an outside straight", false));
                    templist.Add(new HelpItem("Two suited cards 10 or higher", false));
                    templist.Add(new HelpItem("Three cards to a straight flush", false));
                    templist.Add(new HelpItem("Two unsuited high cards (more than two, use lowest)", false));
                    templist.Add(new HelpItem("Suited high cards 10-J, 10-Q, 10-K", false));
                    templist.Add(new HelpItem("One Jack or higher", false));
                    templist.Add(new HelpItem("Discard all dealt cards", false));
                    break;
                case "BONUSPOKER":
                    Title = "BONUS POKER";
                    templist.Add(new HelpItem("Bonus Poker payout is 40:1 for four 2's, 3's, and 4's and 80:1 for four Aces. In extended play, the increased payout for the 4 of a kinds don't make up for the lower payouts on full houses and flushes. 4 of a kind's are dealt approximately every 400 hands so if they aren't hitting more often than that, you are losing money.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("If you aren't dealt a paying hand, check the list of ranked possible combinations listed below and use the one that will produce the highest result.", true));
                    templist.Add(new HelpItem("Four of a kind, straight flush, royal flush", false));
                    templist.Add(new HelpItem("Four cards to a royal flush", false));
                    templist.Add(new HelpItem("Three of a kind, straight, flush, full house", false));
                    templist.Add(new HelpItem("Four cards to a straight flush", false));
                    templist.Add(new HelpItem("Two pair", false));
                    templist.Add(new HelpItem("Pair of Jacks or better", false));
                    templist.Add(new HelpItem("Three cards to a royal flush", false));
                    templist.Add(new HelpItem("Four cards to a flush", false));
                    templist.Add(new HelpItem("Low pair", false));
                    templist.Add(new HelpItem("Four cards to an outside straight", false));
                    templist.Add(new HelpItem("Two suited cards 10 or higher", false));
                    templist.Add(new HelpItem("Three cards to a straight flush", false));
                    templist.Add(new HelpItem("Two unsuited high cards (more than two, use lowest)", false));
                    templist.Add(new HelpItem("Suited high cards 10-J, 10-Q, 10-K", false));
                    templist.Add(new HelpItem("One Jack or higher", false));
                    templist.Add(new HelpItem("Discard all dealt cards", false));
                    break;
                case "JACKSORBETTER":
                    Title = "JACKS OR BETTER";
                    templist.Add(new HelpItem("Jacks or Better is a game between you and the dealer and is played with a standard deck of 52 cards. Five cards are dealt in the first hand. Hold as many cards as you want to build the strongest hand. The remaining cards will be discarded and replaced with new ones from the same playing deck.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("If you have been dealt four of a kind, straight flush, or royal flush then hold all. Otherwise, make the most from the cards contained in your hand by using the following priorities:", true));
                    templist.Add(new HelpItem("Hold four cards to a royal flush", false));
                    templist.Add(new HelpItem("Hold three of a kind, straight, flush, full house", false));
                    templist.Add(new HelpItem("Hold four card straight flushes", false));
                    templist.Add(new HelpItem("Hold two pair", false));
                    templist.Add(new HelpItem("Hold any pair Jacks or better", false));
                    templist.Add(new HelpItem("Hold three card royal flushes", false));
                    templist.Add(new HelpItem("Hold four card flushes", false));
                    templist.Add(new HelpItem("Hold any pair", false));
                    templist.Add(new HelpItem("Hold four card straights", false));
                    templist.Add(new HelpItem("Hold two suited face cards", false));
                    templist.Add(new HelpItem("Hold three to a straight flush", false));
                    templist.Add(new HelpItem("Hold two unsuited face cards (if there's more than two, pick the two lowest)", false));
                    templist.Add(new HelpItem("Hold suited 10/J, 10/Q, or 10/K", false));
                    templist.Add(new HelpItem("Hold one face card", false));
                    templist.Add(new HelpItem("Discard all dealt cards", false));
                    break;
                case "BONUSPOKERDELUXE":
                    Title = "BONUS POKER DELUXE";
                    templist.Add(new HelpItem("Jacks or Better is a game between you and the dealer and is played with a standard deck of 52 cards. Five cards are dealt in the first hand. Hold as many cards as you want to build the strongest hand. The remaining cards will be discarded and replaced with new ones from the same playing deck.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("If you have been dealt four of a kind, straight flush, or royal flush then hold all. Otherwise, make the most from the cards contained in your hand by using the following priorities:", true));
                    templist.Add(new HelpItem("Hold four cards to a royal flush", false));
                    templist.Add(new HelpItem("Hold three of a kind, straight, flush, full house", false));
                    templist.Add(new HelpItem("Hold four card straight flushes", false));
                    templist.Add(new HelpItem("Hold two pair", false));
                    templist.Add(new HelpItem("Hold any pair Jacks or better", false));
                    templist.Add(new HelpItem("Hold three card royal flushes", false));
                    templist.Add(new HelpItem("Hold four card flushes", false));
                    templist.Add(new HelpItem("Hold any pair", false));
                    templist.Add(new HelpItem("Hold four card straights", false));
                    templist.Add(new HelpItem("Hold two suited face cards", false));
                    templist.Add(new HelpItem("Hold three to a straight flush", false));
                    templist.Add(new HelpItem("Hold two unsuited face cards (if there's more than two, pick the two lowest)", false));
                    templist.Add(new HelpItem("Hold suited 10/J, 10/Q, or 10/K", false));
                    templist.Add(new HelpItem("Hold one face card", false));
                    templist.Add(new HelpItem("Discard all dealt cards", false));
                    break;
                case "JOKERPOKER":
                    Title = "JOKER POKER";
                    templist.Add(new HelpItem("Joker Poker is played with a 53 card deck including the Joker. The Joker is wild and stands in for cards of any denomination and suit to complete a winning hand.", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("If you have been dealt (with or without Joker) a royal flush, five of a kind, four of a kind, straight flush, full house, flush, or straight then hold all. Otherwise, make the most from the cards contained in your hand by using the following priorities:", true));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("IF YOU HAVE A JOKER", true));
                    templist.Add(new HelpItem("Hold four cards to a royal flush with Joker", false));
                    templist.Add(new HelpItem("Hold four cards to a straight flush ", false));
                    templist.Add(new HelpItem("Hold three of a kind", false));
                    templist.Add(new HelpItem("Hold three card straight flushes", false));
                    templist.Add(new HelpItem("Hold four card straights", false));
                    templist.Add(new HelpItem("Hold four card flushes", false));
                    templist.Add(new HelpItem("Hold three card royal flushes", false));
                    templist.Add(new HelpItem("Hold inside straights with three or more cards 10 or higher", false));
                    templist.Add(new HelpItem("Hold low pairs for three of a kind", false));
                    templist.Add(new HelpItem("Hold high card pairs", false));
                    templist.Add(new HelpItem("Hold just the Joker", false));
                    templist.Add(new HelpItem("", false));
                    templist.Add(new HelpItem("IF YOU DON'T HAVE A JOKER", true));
                    templist.Add(new HelpItem("Hold four cards to a royal flush with Joker", false));
                    templist.Add(new HelpItem("Hold four cards to a straight flush ", false));
                    templist.Add(new HelpItem("Hold three of a kind", false));
                    templist.Add(new HelpItem("Hold two pairs", false));
                    templist.Add(new HelpItem("Hold three card royal flushes", false));
                    templist.Add(new HelpItem("Hold three card straight flushes", false));
                    templist.Add(new HelpItem("Hold four card flushes", false));
                    templist.Add(new HelpItem("Hold four card straights", false));
                    templist.Add(new HelpItem("Hold any pair", false));
                    templist.Add(new HelpItem("Hold two card straight flushes", false));
                    templist.Add(new HelpItem("Hold two card royal flushes", false));
                    templist.Add(new HelpItem("Hold three card flushes", false));
                    templist.Add(new HelpItem("Hold three card straights", false));
                    templist.Add(new HelpItem("Discard all dealt cards", false));
                    break;
            }
            return templist;
        }
    }
}
