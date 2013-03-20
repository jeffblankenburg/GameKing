using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLogic
{
    public class HandHistory
    {
        public int Id { get; set; }
        public string MicrosoftAccountID { get; set; }
        public int Credits { get; set; }
        public string GameType { get; set; }
        public int OpenHandCard1 { get; set; }
        public int OpenHandCard2 { get; set; }
        public int OpenHandCard3 { get; set; }
        public int OpenHandCard4 { get; set; }
        public int OpenHandCard5 { get; set; }
        public int CloseHandCard1 { get; set; }
        public int CloseHandCard2 { get; set; }
        public int CloseHandCard3 { get; set; }
        public int CloseHandCard4 { get; set; }
        public int CloseHandCard5 { get; set; }
        public bool HeldCard1 { get; set; }
        public bool HeldCard2 { get; set; }
        public bool HeldCard3 { get; set; }
        public bool HeldCard4 { get; set; }
        public bool HeldCard5 { get; set; }
        public string Outcome { get; set; }
        public bool IsSnapped { get; set; }
        public string Platform { get; set; }
        public DateTime DatePlayed { get; set; }

        public HandHistory()
        {

        }

        public HandHistory(string MicrosoftAccount, int CreditCount, Hand Open, Hand Close, string GameName, DateTime HandDate, bool SnapFlag, string platform)
        {
            MicrosoftAccountID = MicrosoftAccount;
            Credits = CreditCount;
            GameType = GameName;

            OpenHandCard1 = Int32.Parse((Open.Cards[0].Suit.ID).ToString() + (Open.Cards[0].Value.Number));
            OpenHandCard2 = Int32.Parse((Open.Cards[1].Suit.ID).ToString() + (Open.Cards[1].Value.Number));
            OpenHandCard3 = Int32.Parse((Open.Cards[2].Suit.ID).ToString() + (Open.Cards[2].Value.Number));
            OpenHandCard4 = Int32.Parse((Open.Cards[3].Suit.ID).ToString() + (Open.Cards[3].Value.Number));
            OpenHandCard5 = Int32.Parse((Open.Cards[4].Suit.ID).ToString() + (Open.Cards[4].Value.Number));

            CloseHandCard1 = Int32.Parse((Close.Cards[0].Suit.ID).ToString() + (Close.Cards[0].Value.Number));
            CloseHandCard2 = Int32.Parse((Close.Cards[1].Suit.ID).ToString() + (Close.Cards[1].Value.Number));
            CloseHandCard3 = Int32.Parse((Close.Cards[2].Suit.ID).ToString() + (Close.Cards[2].Value.Number));
            CloseHandCard4 = Int32.Parse((Close.Cards[3].Suit.ID).ToString() + (Close.Cards[3].Value.Number));
            CloseHandCard5 = Int32.Parse((Close.Cards[4].Suit.ID).ToString() + (Close.Cards[4].Value.Number));

            HeldCard1 = Close.Held[0];
            HeldCard2 = Close.Held[1];
            HeldCard3 = Close.Held[2];
            HeldCard4 = Close.Held[3];
            HeldCard5 = Close.Held[4];

            Outcome = Close.Check(GameName);
            IsSnapped = SnapFlag;
            Platform = platform;

            DatePlayed = HandDate;
        }
    }
}
