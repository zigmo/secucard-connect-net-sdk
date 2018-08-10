using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class User : SecuObject
    {
        [DataMember(Name = "salutation")]
        public string Salutation { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "forename")]
        public string Forename { get; set; }

        [DataMember(Name = "surname")]
        public string Surname { get; set; }

        [DataMember(Name = "companyname")]
        public string CompanyName { get; set; }

        public override string ToString()
        {
            return "User{" +
                   ", companyName='" + CompanyName + '\'' +
                   ", title='" + Title + '\'' +
                   ", salutation='" + Salutation + '\'' +
                   ", forename='" + Forename + '\'' +
                   ", surname='" + Surname + '\'' +
                   "} " + base.ToString();
        }
    }
}