using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLogic
{
    public class Player
    {
        public int CurrentBet = 1;
        public int Credits = 10000;

        public bool IncreaseBet(int BetAmount)
        {
            if (BetAmount == 1)
            {
                CurrentBet += 1;
                if (CurrentBet == 5) return true;
                else if (CurrentBet > 5) CurrentBet = 1;
                return false;
            }
            else
            {
                CurrentBet = 5;
                return true;
            }
        }
    }
}
