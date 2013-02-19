using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameKing
{
    class Player
    {
        public int CurrentBet = 0;
        public int Credits = 10000;

        public void IncreaseBet(int BetAmount)
        {
            if (BetAmount == 1)
            {
                CurrentBet += 1;
                if (CurrentBet > 5) CurrentBet = 1;
            }
            else
                CurrentBet = 5;
        }
    }
}
