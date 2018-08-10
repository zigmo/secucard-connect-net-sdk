using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Loyalty.Model
{
    [DataContract]
    public class Bonus
    {
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "balance")]
        public int Balance { get; set; }

        /**   * PTS or EUR   */

        [DataMember(Name = "currency")]
        public string Currency { get; set; }
    }
}