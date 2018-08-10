using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Model;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Service;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    public class SecupayPrepaysService : PaymentService<SecupayPrepay>
    {
        public static readonly ServiceMetaData<SecupayPrepay> MetaData = new ServiceMetaData<SecupayPrepay>("payment", "secupayprepays");

        protected override ServiceMetaData<SecupayPrepay> GetMetaData()
        {
            return MetaData;
        }
    }
}
