using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Model
{
    [DataContract]
    public class BasketInfo
    {
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "sum")]
        public int Sum { get; set; }

        public override string ToString()
        {
            return "BasketInfo{" +
                   "sum=" + Sum + ", currency=" + Currency +
                   '}';
        }
    }
}