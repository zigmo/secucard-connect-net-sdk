using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class ResultClass : SecuObject
    {
        [DataMember(Name = "result")]
        public bool? Result { get; set; }
    }
}