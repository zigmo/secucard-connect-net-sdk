using System;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    [DataContract]
    public class Container : SecuObject
    {
        public const string TypeBankAccount = "bank_account";

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }
        
        [System.Obsolete("was not processed")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "private")]
        public Data PrivateData { get; set; }

        [DataMember(Name = "public")]
        public Data PublicData { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "mandate")]
        public Mandate Mandate { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return this.Created.ToDateTimeZone(); }
            set { this.Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return this.Updated.ToDateTimeZone(); }
            set { this.Updated = value.ToDateTime(); }
        }

        public DateTime? Updated { get; set; }

        public override string ToString()
        {
            return "Container{" +
                   ", privateData=" + this.PrivateData +
                   ", publicData=" + this.PublicData +
                   ", type='" + this.Type + '\'' +
                   ", mandate=" + this.Mandate +
                   ", created=" + this.Created +
                   ", updated=" + this.Updated +
                   '}';
        }
    }
}