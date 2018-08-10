using System;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment.Event
{
    public delegate void PaymentEventHandler<T>(object sender, PaymentEventEventArgs<T> args);

    public class PaymentEventEventArgs<T> : EventArgs
    {
        public Event<T[]> SecucardEvent { get; set; }
    }
}