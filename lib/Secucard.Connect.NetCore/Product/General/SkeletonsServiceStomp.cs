using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Net;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General
{
    public class SkeletonsServiceStomp : ProductService<Skeleton>
    {
        public static readonly ServiceMetaData<Skeleton> MetaData = new ServiceMetaData<Skeleton>(
            "general", "skeletons");

        protected override ServiceMetaData<Skeleton> GetMetaData()
        {
            return MetaData;
        }

        protected override ChannelOptions GetDefaultOptions()
        {
            var channelOptions = base.GetDefaultOptions();
            channelOptions.Channel = ChannelOptions.ChannelStomp;
            return channelOptions;
        }
    }
}