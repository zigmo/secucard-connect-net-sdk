using System;

namespace Zigmo.Secucard.Connect.NetCore.Net.Stomp.Client
{
    public delegate void StompCoreFrameArrivedEventHandler(object sender, StompCoreFrameArrivedEventArgs args);

    public class StompCoreFrameArrivedEventArgs : EventArgs
    {
        public StompFrame Frame { get; set; }
        public DateTime Time { get; set; }
    }
}