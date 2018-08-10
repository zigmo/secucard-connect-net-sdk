using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    public class PaymentTransactionsService : ProductService<PaymentTransaction>
    {
        public static readonly ServiceMetaData<PaymentTransaction> MetaData = new ServiceMetaData<PaymentTransaction>("payment",
            "transactions");

        protected override ServiceMetaData<PaymentTransaction> GetMetaData()
        {
            return MetaData;
        }
    }
}