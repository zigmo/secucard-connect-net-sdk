using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Model;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Service;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    public class SecupayDebitsService : PaymentService<SecupayDebit>
    {
        public static readonly ServiceMetaData<SecupayDebit> MetaData = new ServiceMetaData<SecupayDebit>("payment", "secupaydebits");

        protected override ServiceMetaData<SecupayDebit> GetMetaData()
        {
            return MetaData;
        }
    }
}