using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Net;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General
{
    public class SkeletonsService : ProductService<Skeleton>
    {
        public static readonly ServiceMetaData<Skeleton> MetaData = new ServiceMetaData<Skeleton>(
            "general", "skeletons");

        protected override ServiceMetaData<Skeleton> GetMetaData()
        {
            return MetaData;
        }

        public void CreateEvent()
        {
            ExecuteToBool("12345", "Demoevent", null,
                new Demoevent
                {
                    Delay = 2,
                    Target = "general.skeletons",
                    Type = "DemoEvent",
                    Data = "{ whatever: \"whole object gets send as payload for event\"}"
                }, new ChannelOptions {Channel = ChannelOptions.ChannelStomp, TimeOutSec = 100});
        }
    }
}