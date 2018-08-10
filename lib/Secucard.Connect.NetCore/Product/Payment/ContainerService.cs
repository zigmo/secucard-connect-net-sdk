using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    public class ContainersService : ProductService<Container>
    {
        public static readonly ServiceMetaData<Container> MetaData = new ServiceMetaData<Container>("payment", "containers");

        protected override ServiceMetaData<Container> GetMetaData()
        {
            return MetaData;
        }
    }
}