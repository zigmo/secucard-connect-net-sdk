using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    /// <summary>
    /// Subscription Data Model class
    /// </summary>
    [DataContract]
    public class Subscription : SecuObject
    {
        [DataMember(Name = "purpose")]
        public string Purpose { get; set; }
    }
}
