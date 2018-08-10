using System;

namespace Zigmo.Secucard.Connect.NetCore.Net.Stomp.Client
{
    public delegate void StompClientFrameArrivedHandler(object sender, StompClientFrameArrivedArgs args);

    public class StompClientFrameArrivedArgs : EventArgs
    {
        public StompFrame Frame { get; set; }
        public DateTime Time { get; set; }
    }
}