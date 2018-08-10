using System;

namespace Zigmo.Secucard.Connect.NetCore.Net.Stomp.Client
{
    public delegate void StompClientStatusChangedEventHandler(object sender, StompClientStatusChangedEventArgs args);

    public class StompClientStatusChangedEventArgs : EventArgs
    {
        public EnumStompClientStatus Status { get; set; }
        public DateTime Time { get; set; }
    }
}