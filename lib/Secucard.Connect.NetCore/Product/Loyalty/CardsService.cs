using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Loyalty.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Loyalty
{
    public class CardsService : ProductService<Card>
    {
        public static readonly ServiceMetaData<Card> MetaData = new ServiceMetaData<Card>("general", "cards");

        protected override ServiceMetaData<Card> GetMetaData()
        {
            return MetaData;
        }
    }
}