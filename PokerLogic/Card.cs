using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PokerLogic
{
    [DataContract]
    public class Card
    {
        [DataMember]
        public Suit Suit { get; set; }
        [DataMember]
        public Value Value { get; set; }

        public Card(Suit suit, Value value)
        {
            this.Suit = suit;
            this.Value = value;
        }
    }
}
