using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Common.Model
{
    [DataContract]
    public class StompResult
    {
        [DataMember(Name = "result")]
        public bool? Result { get; set; }
    }
}