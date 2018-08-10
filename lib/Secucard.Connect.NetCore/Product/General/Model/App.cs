using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class App : SecuObject
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}