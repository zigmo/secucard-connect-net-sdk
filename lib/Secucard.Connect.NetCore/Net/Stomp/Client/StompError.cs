using System;
using System.Collections.Generic;

namespace Zigmo.Secucard.Connect.NetCore.Net.Stomp.Client
{
    /// <summary>
    ///     Exception wrapping a STOMP error frame.
    /// </summary>
    public class StompError : Exception
    {
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public StompError(string body, Dictionary<string, string> headers)
        {
            Body = body;
            Headers = headers;
        }
    }
}