using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Model
{
    [DataContract]
    public class SmartPin : SecuObject
    {
        [DataMember(Name = "user_pin")]
        public string UserPin { get; set; }
    }
}