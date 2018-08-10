using System;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Loyalty.Model
{
    [DataContract]
    public class Card : SecuObject
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "cardNumber")]
        public string CardNumber { get; set; }

        public DateTime? Created { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }
    }
}