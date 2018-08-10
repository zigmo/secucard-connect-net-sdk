using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class OpenHours
    {
        [DataMember(Name = "close")]
        public DayTime Close { get; set; }

        [DataMember(Name = "open")]
        public DayTime Open { get; set; }

        public override string ToString()
        {
            return string.Format("Open: {0}, Close: {1}", Open, Close);
        }
    }
}