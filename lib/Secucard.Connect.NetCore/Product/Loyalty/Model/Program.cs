using System.Collections.Generic;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Loyalty.Model
{
    [DataContract]
    public class Program : SecuObject
    {
        [DataMember(Name = "cardGroup")]
        public CardGroup CardGroup { get; set; }

        [DataMember(Name = "conditions")]
        public List<Condition> Conditions { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}