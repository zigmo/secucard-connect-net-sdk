using System;

namespace Zigmo.Secucard.Connect.NetCore.Net.Stomp.Client
{
    /// <summary>
    ///     Indicates that no receipt for a message was received in time.
    /// </summary>
    public class NoReceiptException : Exception
    {
        public NoReceiptException()
        {
        }

        public NoReceiptException(string message) : base(message)
        {
        }
    }
}