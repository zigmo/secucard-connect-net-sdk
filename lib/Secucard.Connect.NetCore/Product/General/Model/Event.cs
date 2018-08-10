using System;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class Event<T> : SecuObject
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "target")]
        public string Target { get; set; }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }

        public override string ToString()
        {
            return "Event{" +
                   "type='" + Type + '\'' +
                   ", target='" + Target + '\'' +
                   ", created=" + Created +
                   ", data=" + Data +
                   '}';
        }
    }
}