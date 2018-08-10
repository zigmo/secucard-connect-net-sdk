using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Model;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Service;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    public class SecupayCreditcardsService : PaymentService<SecupayCreditcard>
    {
        public static readonly ServiceMetaData<SecupayCreditcard> MetaData = new ServiceMetaData<SecupayCreditcard>("payment", "secupaycreditcards");

        protected override ServiceMetaData<SecupayCreditcard> GetMetaData()
        {
            return MetaData;
        }
    }
}