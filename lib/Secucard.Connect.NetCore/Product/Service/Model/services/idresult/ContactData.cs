using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Service.Model.services.idresult
{
    [DataContract]
    public class ContactData
    {
        [DataMember(Name = "mobile")]
        public string Mobile { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        public override string ToString()
        {
            return "ContactData{" +
                   "mobilePhone='" + Mobile + '\'' +
                   ", email='" + Email + '\'' +
                   '}';
        }
    }
}