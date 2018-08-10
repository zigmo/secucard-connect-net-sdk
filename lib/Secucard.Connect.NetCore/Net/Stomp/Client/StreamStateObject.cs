using System.Collections.Generic;
using System.Net.Security;

namespace Zigmo.Secucard.Connect.NetCore.Net.Stomp.Client
{
    public class StreamStateObject
    {
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] Buffer = new byte[BufferSize];
        // Received data so far
        public List<byte> Bytes = new List<byte>();
        // Client socket.
        public SslStream Stream = null;
    }
}