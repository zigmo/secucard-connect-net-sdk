using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Model
{
    [DataContract]
    public class ReceiptLineValue
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        [DataMember(Name = "decoration")]
        public List<string> Decoration { get; set; }
    }
}