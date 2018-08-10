using System;

namespace Zigmo.Secucard.Connect.NetCore.Net.Stomp.Client
{
    public delegate void StompCoreExceptionEventHandler(object sender, StompCoreExceptionEventArgs args);

    public class StompCoreExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
        public DateTime Time { get; set; }
    }
}