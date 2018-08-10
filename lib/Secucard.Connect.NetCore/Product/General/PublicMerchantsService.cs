using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General
{
    public class PublicMerchantsService : ProductService<PublicMerchant>
    {
        public static readonly ServiceMetaData<PublicMerchant> MetaData = new ServiceMetaData<PublicMerchant>(
            "general", "publicmerchants");

        protected override ServiceMetaData<PublicMerchant> GetMetaData()
        {
            return MetaData;
        }
    }
}