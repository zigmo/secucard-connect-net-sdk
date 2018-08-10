using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class AccountDevice : SecuObject
    {
        [DataMember(Name = "platform")]
        public string Platform { get; set; }

        [DataMember(Name = "uid")]
        public string Uid { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "info")]
        public List<NameValueItem> Info { get; set; }

        [DataMember(Name = "online")]
        public bool Online { get; set; }
    }
}