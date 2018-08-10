using System;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Service.Model.services
{
    [DataContract]
    public class IdentContract : SecuObject
    {
        [DataMember(Name = "demo")]
        public bool? Demo { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "redirect_url_success")]
        public string RedirectUrlSuccess { get; set; }

        [DataMember(Name = "redirect_url_failed")]
        public string RedirectUrlFailed { get; set; }

        [DataMember(Name = "push_url")]
        public string PushUrl { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }
    }
}