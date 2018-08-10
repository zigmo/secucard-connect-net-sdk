using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General
{
    public class MerchantsService : ProductService<Merchant>
    {
        public static readonly ServiceMetaData<Merchant> MetaData = new ServiceMetaData<Merchant>("general", "merchants");

        protected override ServiceMetaData<Merchant> GetMetaData()
        {
            return MetaData;
        }
    }
}