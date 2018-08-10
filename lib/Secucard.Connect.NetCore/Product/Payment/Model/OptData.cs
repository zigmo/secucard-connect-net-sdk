using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    [DataContract]
    public class OptData : SecuObject
    {
        [DataMember(Name = "has_accepted_disclaimer")]
        public bool DisclaimerAccepted { get; set; }

        [DataMember(Name = "hide_disclaimer")]
        public bool HideDisclaimer { get; set; }
    }
}
