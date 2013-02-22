using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PokerLogic
{
    [DataContract]
    public class Suit
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int ID { get; set; }
    }
}
