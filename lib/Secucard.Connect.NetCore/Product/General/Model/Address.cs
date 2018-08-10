using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class Address
    {
        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "street_number")]
        public string StreetNumber { get; set; }

        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; } // ISO 3166 country code like DE

        public override string ToString()
        {
            return "Address{" +
                   "street='" + Street + '\'' +
                   ", streetNumber='" + StreetNumber + '\'' +
                   ", postalCode='" + PostalCode + '\'' +
                   ", city='" + City + '\'' +
                   ", country='" + Country + '\'' +
                   '}';
        }
    }
}