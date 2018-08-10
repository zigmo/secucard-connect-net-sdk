using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "lat")]
        public double Lat { get; set; }

        [DataMember(Name = "lon")]
        public double Lon { get; set; }

        [DataMember(Name = "accuracy")]
        public float Accuracy { get; set; }
    }
}