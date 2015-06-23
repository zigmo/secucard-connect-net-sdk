namespace Secucard.Model.General
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Location
    {

        [DataMember(Name = "lat")]
        public double Lat { get; set; }

        [DataMember(Name = "lon")]
        public double Lon { get; set; }

        [DataMember(Name = "accuracy")]
        public float Accuracy { get; set; }

        //public Location() {
        //}

        //public Location(double lat, double lon, float accuracy) {
        //    this.lat = lat;
        //    this.lon = lon;
        //    this.accuracy = accuracy;
        //}


    }
}