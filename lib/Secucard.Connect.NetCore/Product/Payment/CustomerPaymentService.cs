using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    public class CustomerPaymentService : ProductService<Customer>
    {
        public static readonly ServiceMetaData<Customer> MetaData = new ServiceMetaData<Customer>("payment", "customers");

        protected override ServiceMetaData<Customer> GetMetaData()
        {
            return MetaData;
        }
    }
}