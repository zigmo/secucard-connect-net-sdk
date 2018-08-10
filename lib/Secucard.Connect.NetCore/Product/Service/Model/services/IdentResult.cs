using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;
using Zigmo.Secucard.Connect.NetCore.Product.Service.Model.services.idresult;

namespace Zigmo.Secucard.Connect.NetCore.Product.Service.Model.services
{
    [DataContract]
    public class IdentResult : SecuObject
    {
        public const string StatusOk = "ok";
        public static string StatusFailed = "failed";
        public static string StatusPreliminaryOk = "ok_preliminary";
        public static string StatusPreliminaryFailed = "failed_preliminary";

        [DataMember(Name = "request")]
        public IdentRequest Request { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "demo")]
        public bool? Demo { get; set; }

        [DataMember(Name = "person")]
        public List<Person> Persons { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "contract")]
        public IdentContract Contract { get; set; }

        public override string ToString()
        {
            return "IdentResult{" +
                   "request=" + Request +
                   ", status='" + Status + '\'' +
                   ", persons=" + Persons +
                   ", created=" + Created +
                   "} " + base.ToString();
        }
    }
}