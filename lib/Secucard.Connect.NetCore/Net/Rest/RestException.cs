using System;

namespace Zigmo.Secucard.Connect.NetCore.Net.Rest
{
    public class RestException : Exception
    {
        public string BodyText { get; set; }
        public int? StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}