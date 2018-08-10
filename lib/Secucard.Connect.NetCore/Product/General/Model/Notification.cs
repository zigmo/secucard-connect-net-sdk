using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class Notification : SecuObject
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        public override string ToString()
        {
            return "Notification{" +
                   "text='" + Text + '\'' +
                   "} " + base.ToString();
        }
    }
}