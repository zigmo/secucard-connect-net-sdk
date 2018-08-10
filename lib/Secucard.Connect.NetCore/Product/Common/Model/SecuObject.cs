using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Common.Model
{
    [DataContract]
    public abstract class SecuObject
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "object")]
        public virtual string Object { get; set; }

        public override string ToString()
        {
            return "SecuObject {id='" + Id + "', object='" + Object + '}';
        }
    }
}