using System;

namespace Zigmo.Secucard.Connect.NetCore.Client
{
    public class SecucardConnectException : Exception
    {
        public SecucardConnectException() : base()
        {
        }

        public SecucardConnectException(string message) : base(message)
        {
        }

        public SecucardConnectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
