using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class NameValueItem
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Name, Value);
        }
    }
}