using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Event;
using Zigmo.Secucard.Connect.NetCore.Product.General.Event;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General
{
    public class AccountDevicesService : ProductService<AccountDevice>
    {
        public static readonly ServiceMetaData<AccountDevice> MetaData = new ServiceMetaData<AccountDevice>("general",
            "accountdevices");

        protected override ServiceMetaData<AccountDevice> GetMetaData()
        {
            return MetaData;
        }

        public AccountDeviceChangedEventHandler AccountDeviceChangedEvent;


        public override void RegisterEvents()
        {
            Context.EventDispatcher.RegisterForEvent(GetType().Name, GetMetaData().ProductResource, Events.TypeChanged,
                OnNewChangedEvent);
        }

        private void OnNewChangedEvent(object obj)
        {
            if (AccountDeviceChangedEvent != null)
                AccountDeviceChangedEvent(this,
                    new AccountDeviceChangedEventArgs {SecucardEvent = (Event<AccountDevice>) obj});
        }
    }
}