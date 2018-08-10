using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class DeliveryAddress : SecuObject
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "zipcode")]
        public string ZipCode { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }
    }
}