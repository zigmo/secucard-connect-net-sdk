using System;

namespace Zigmo.Secucard.Connect.NetCore.Net.Stomp
{
    public delegate void StompEventArrivedEventHandler(object sender, StompEventArrivedEventArgs args);

    public class StompEventArrivedEventArgs : EventArgs
    {
        public string Body { get; set; }
        public DateTime Time { get; set; }
    }
}