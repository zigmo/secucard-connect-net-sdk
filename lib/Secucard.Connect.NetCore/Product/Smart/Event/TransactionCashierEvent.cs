using System;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart.Event
{
    public delegate void TransactionCashierEventHandler(object sender, TransactionCashierEventArgs args);

    public class TransactionCashierEventArgs : EventArgs
    {
        public Event<Notification> SecucardEvent { get; set; }
    }
}