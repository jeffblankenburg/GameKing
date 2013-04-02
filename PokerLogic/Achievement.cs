using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLogic
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public int Points { get; set; }
        public string GameType { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }

        public Achievement()
        {
            IsCompleted = false;
        }
    }
}
