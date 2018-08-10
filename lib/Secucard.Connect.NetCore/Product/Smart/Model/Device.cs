using System;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Model
{
    [DataContract]
    public class Device : SecuObject
    {
        public DateTime? Created;

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "device")]
        public General.Model.Device GenralDevice { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "online")]
        public bool Online { get; set; }

        [DataMember(Name = "store")]
        public Store Store { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "user_pin")]
        public string UserPin { get; set; }

        [DataMember(Name = "vendor")]
        public string Vendor { get; set; }

        [DataMember(Name = "vendor_uid")]
        public string VendorUid { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public override string ToString()
        {
            return "Device{" +
                   "type='" + Type + '\'' +
                   '}';
        }
    }
}