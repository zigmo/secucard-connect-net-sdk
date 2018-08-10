using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Model;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Service;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    public class SecupayInvoicesService : PaymentService<SecupayInvoice>
    {
        public static readonly ServiceMetaData<SecupayInvoice> MetaData = new ServiceMetaData<SecupayInvoice>("payment", "secupayinvoices");

        protected override ServiceMetaData<SecupayInvoice> GetMetaData()
        {
            return MetaData;
        }
    }
}