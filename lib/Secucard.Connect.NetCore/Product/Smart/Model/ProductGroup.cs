using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Model
{
    [DataContract]
    public class ProductGroup
    {
        [DataMember(Name = "desc")]
        public string Desc { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }

        public override string ToString()
        {
            return "ProductGroup{" +
                   "id='" + Id + '\'' +
                   ", desc='" + Desc + '\'' +
                   ", level='" + Level + '\'' +
                   '}';
        }
    }
}