using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    [DataContract]
    public class SecupayDebit : Transaction
    {
        [DataMember(Name = "container")]
        public Container Container { get; set; }

        public override string ToString()
        {
            return "SecupayDebit{" +
                   "container=" + Container +
                   "} " + base.ToString();
        }
    }
}