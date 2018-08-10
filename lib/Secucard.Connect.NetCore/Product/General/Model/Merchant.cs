using System.Collections.Generic;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class Merchant : SecuObject
    {
        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "metadata")]
        public MetaData Metadata { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "photo")]
        public List<string> Photo { get; set; }

        [DataMember(Name = "photo_main")]
        public string PhotoMain { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}