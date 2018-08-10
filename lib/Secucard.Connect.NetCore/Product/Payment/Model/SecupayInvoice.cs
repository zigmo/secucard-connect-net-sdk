using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    [DataContract]
    public class SecupayInvoice : Transaction
    {
        [DataMember(Name = "container")]
        public Container Container { get; set; }

        public override string ToString()
        {
            return "SecupayInvoice{" +
                   "container=" + Container +
                   "} " + base.ToString();
        }
    }
}