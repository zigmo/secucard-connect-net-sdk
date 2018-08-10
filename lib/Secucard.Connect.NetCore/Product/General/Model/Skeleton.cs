using System.Collections.Generic;
using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Model
{
    [DataContract]
    public class Skeleton : SecuObject
    {
        [DataMember(Name = "a")]
        public string A { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "b")]
        public string B { get; set; }

        [DataMember(Name = "c")]
        public string C { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        [DataMember(Name = "skeleton_list")]
        public List<Skeleton> SkeletonList { get; set; }

        [DataMember(Name = "skeleton")]
        public Skeleton SkeletonObj { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }


        public override string ToString()
        {
            return "Skeleton{" +
                   ", id='" + Id + '\'' +
                   ", a='" + A + '\'' +
                   ", b='" + B + '\'' +
                   ", c='" + C + '\'' +
                   ", amount=" + Amount +
                   ", picture='" + Picture + '\'' +
                   ", date='" + Date + '\'' +
                   ", type='" + Type + '\'' +
                   ", location=" + Location +
                   ", skeleton=" + SkeletonObj +
                   ", skeleton_list=" + SkeletonList +
                   '}';
        }
    }
}