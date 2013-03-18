using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLogic
{
    public class HelpContent
    {
        public string Title { get; set; }
        public List<string> HelpItems { get; set; }

        public HelpContent()
        {
            
        }

        public HelpContent(string GameType)
        {
            HelpItems = LoadHelp(GameType);
        }

        private List<string> LoadHelp(string GameType)
        {
            List<string> templist = new List<string>();
            
            switch (GameType)
            {
                case "DEUCESWILD":
                    Title = "DEUCES WILD";
                    templist.Add("FOUR DEUCES");
                    templist.Add("Always keep four deuces.  Always.");
                    templist.Add("");
                    templist.Add("THREE DEUCES");
                    templist.Add("Keep a royal flush, otherwise keep only the three 2s.");
                    templist.Add("");
                    templist.Add("TWO DEUCES");
                    templist.Add("Keep any four of a kind or higher.");
                    templist.Add("Keep four to a royal flush.");
                    templist.Add("Keep four to a straight flush with 2 consecutive cards, 6-7 or higher.");
                    templist.Add("Otherwise just keep the two 2s.");
                    templist.Add("");
                    templist.Add("ONE DEUCE");
                    templist.Add("Keep any four of a kind.");
                    templist.Add("Keep four to a royal flush.");
                    templist.Add("Full house.");
                    templist.Add("Keep four to a straight flush with 3 consecutive cards, 5-7 or higher.");
                    templist.Add("Keep three of a kind.");
                    templist.Add("Keep a straight.");
                    templist.Add("Keep a flush.");
                    templist.Add("Keep four to any straight flush.");
                    templist.Add("Keep three to a royal flush.");
                    templist.Add("Keep three to a straight flush with 2 consecutive cards, 6-7 or higher.");
                    templist.Add("Keep just the 2.");
                    templist.Add("");
                    templist.Add("ZERO DEUCES");
                    templist.Add("Four or five cards to a royal flush.");
                    templist.Add("Keep any straight flush - three of a kind.");
                    templist.Add("Keep four to a straight flush.");
                    templist.Add("Keep 3 to a royal flush.");
                    templist.Add("Keep a pair.");
                    templist.Add("Keep four to a flush.");
                    templist.Add("Keep four to an outside straight.");
                    templist.Add("Keep three to a straight flush.");
                    templist.Add("Keep four to an inside straight, except when you a missing a deuce.");
                    templist.Add("Keep two to a royal flush.");
                    templist.Add("Keep none of the cards.  Get five new ones.");
                    break;
            }

            return templist;
        }
    }
}
