using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameKing
{
    [DataContract]
    public class Value
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Number { get; set; }
    }
}
