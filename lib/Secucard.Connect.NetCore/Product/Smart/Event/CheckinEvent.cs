using System;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;
using Zigmo.Secucard.Connect.NetCore.Product.Smart.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Event
{
    public delegate void CheckinEventHandler(object sender, CheckinEventEventArgs args);

    public class CheckinEventEventArgs : EventArgs
    {
        public Event<Checkin> SecucardEvent { get; set; }
    }
}