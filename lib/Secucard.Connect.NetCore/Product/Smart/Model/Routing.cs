using System.Collections.Generic;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Model
{
    [DataContract]
    public class Routing : SecuObject
    {
        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "store")]
        public Store Store { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "assign")]
        public List<Device> Assign { get; set; }
    }
}