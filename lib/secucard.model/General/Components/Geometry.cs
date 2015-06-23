namespace Secucard.Model.General.Components
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Geometry
    {
        [DataMember(Name = "lat")]
        public double Lat;

        [DataMember(Name = "lon")]
        public double Lon;

        //public Geometry()
        //{
        //}

        //public Geometry(double lat, double lon)
        //{
        //    Lat = lat;
        //    Lon = lon;
        //}


    }
}
