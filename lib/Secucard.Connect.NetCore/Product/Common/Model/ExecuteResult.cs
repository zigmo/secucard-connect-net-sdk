using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Product.Common.Model
{
    /// <summary>
    /// Generic result container used as payload (data) 
    /// </summary>
    [DataContract]
    public class ExecuteResult
    {
        [DataMember(Name = "result")]
        public string Result { get; set; }

        [DataMember(Name = "request")]
        public string Request { get; set; }
    }
}