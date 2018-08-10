using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class Email
    {
        [DataMember(Name = "email_formatted")]
        public string EmailFormatted { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}