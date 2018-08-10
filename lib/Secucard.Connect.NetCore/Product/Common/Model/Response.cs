using System.Runtime.Serialization;
using Zigmo.Secucard.Connect.NetCore.Net.Util;

namespace Zigmo.Secucard.Connect.NetCore.Product.Common.Model
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "data")]
        public string Data { get; set; }

        public Response(string json)
        {
            // On return data contains an unknown object that will be treated as a string at first.
            // Workaround: MS json serializer does not have the option to convert object to string
            var dict = new JsonSplitter().CreateDictionary(json);
            if (dict.ContainsKey("status")) Status = dict["status"];
            if (dict.ContainsKey("data")) Data = dict["data"];
        }
    }
}