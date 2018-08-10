using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class DayTime
    {
        [DataMember(Name = "day")]
        public int Day { get; set; }

        [DataMember(Name = "time")]
        public string Time { get; set; }

        public override string ToString()
        {
            return string.Format("Day: {0}, Time: {1}", Day, Time);
        }
    }
}