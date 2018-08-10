using System;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General.Event
{
    public delegate void AccountDeviceChangedEventHandler(object sender, AccountDeviceChangedEventArgs args);

    public class AccountDeviceChangedEventArgs : EventArgs
    {
        public Event<AccountDevice> SecucardEvent { get; set; }
    }
}