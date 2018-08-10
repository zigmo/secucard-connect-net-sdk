using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    /// <summary>
    /// Experience Data Model class
    /// </summary>
    [DataContract]
    public class Experience : SecuObject
    {
        /// <summary>
        /// The number of positive customer experiences
        /// </summary>
        [DataMember(Name = "positiv")]
        public int Positiv { get; set; }

        /// <summary>
        /// The number of negative customer experiences (open orders)
        /// </summary>
        [DataMember(Name = "negativ")]
        public int Negativ { get; set; }
    }
}
