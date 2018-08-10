using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    [DataContract]
    public class SecupayCreditcard : Transaction
    {
        [DataMember(Name = "container")]
        public Container Container { get; set; }

        public override string ToString()
        {
            return "SecupayCreditcard{" +
                   "container=" + Container +
                   "} " + base.ToString();
        }
    }
}