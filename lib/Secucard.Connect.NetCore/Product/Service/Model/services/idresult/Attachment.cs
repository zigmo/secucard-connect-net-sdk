using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Service.Model.services.idresult
{
    [DataContract]
    public class Attachment : MediaResource
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}