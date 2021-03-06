﻿namespace Secucard.Connect.Net.Stomp.Client
{
    using System;

    public delegate void StompClientFrameArrivedHandler(object sender, StompClientFrameArrivedArgs args);

    public class StompClientFrameArrivedArgs : EventArgs
    {
        public StompFrame Frame { get; set; }
        public DateTime Time { get; set; }
    }
}