using System;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Model
{
    [DataContract]
    public class Customer : SecuObject
    {
        [DataMember(Name = "contact")]
        public Contact Contact { get; set; }

        [DataMember(Name = "contract")]
        public Contract Contract { get; set; }

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

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        public override string ToString()
        {
            return "Customer{" +
                   ", contact=" + Contact +
                   ", created=" + this.Created +
                   ", updated=" + this.Updated +
                   ", contract=" + Contract +
                   "} " + base.ToString();
        }
    }
}
