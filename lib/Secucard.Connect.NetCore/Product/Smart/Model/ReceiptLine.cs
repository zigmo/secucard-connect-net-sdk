using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Model
{
    [DataContract]
    public class ReceiptLine
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "value")]
        public ReceiptLineValue Value { get; set; }

        public override string ToString()
        {
            return "ReceiptLine{" +
                   "type='" + Type + '\'' +
                   ", value='" + Value + '\'' +
                   '}';
        }
    }
}