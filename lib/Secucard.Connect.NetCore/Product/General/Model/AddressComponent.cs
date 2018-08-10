using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class AddressComponent
    {
        [DataMember(Name = "long_name")]
        public string LongName { get; set; }

        [DataMember(Name = "short_name")]
        private string ShortName { get; set; }

        [DataMember(Name = "types")]
        private List<string> Types { get; set; }

        public override string ToString()
        {
            return string.Format("LongName: {0}, ShortName: {1}", LongName, ShortName);
        }
    }
}