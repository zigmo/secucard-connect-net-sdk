using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Service.Model.services.idresult
{
    [DataContract]
    public class IdentificationDocument
    {
        [DataMember(Name = "identificationprocess")]
        private ValueClass Country { get; set; }

        [DataMember(Name = "dateissued")]
        private ValueClass DateIssued { get; set; }

        [DataMember(Name = "issuedby")]
        private ValueClass IssuedBy { get; set; }

        [DataMember(Name = "number")]
        private ValueClass Number { get; set; }

        [DataMember(Name = "type")]
        private ValueClass Type { get; set; }

        [DataMember(Name = "validuntil")]
        private ValueClass ValidUntil { get; set; }

        public override string ToString()
        {
            return "IdentificationDocument{" +
                   "country=" + Country +
                   ", dateIssued=" + DateIssued +
                   ", issuedBy=" + IssuedBy +
                   ", number=" + Number +
                   ", type=" + Type +
                   ", validUntil=" + ValidUntil +
                   '}';
        }
    }
}