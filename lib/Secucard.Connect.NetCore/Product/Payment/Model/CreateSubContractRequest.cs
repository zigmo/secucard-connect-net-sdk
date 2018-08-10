using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    [DataContract]
    public class CreateSubContractRequest
    {
        [DataMember(Name = "contact")]
        public Contact Contact { get; set; }

        [DataMember(Name = "project")]
        public string Project { get; set; }

        [DataMember(Name = "payout_account")]
        public Data PayoutAccount { get; set; }

        [DataMember(Name = "iframe_opts")]
        public IframeOptData IframeOpts { get; set; }

        [DataMember(Name = "payin_account")]
        public bool PayinAccount { get; set; }

        public override string ToString()
        {
            return "CreateSubContractRequest{" +
                   "contact='" + this.Contact + '\'' +
                   ", project='" + this.Project + '\'' +
                   ", payout_account='" + this.PayoutAccount + '\'' +
                   ", iframe_opts='" + this.IframeOpts + '\'' +
                   ", payin_account='" + this.PayinAccount + '\'' +
                   '}';
        }
    }
}