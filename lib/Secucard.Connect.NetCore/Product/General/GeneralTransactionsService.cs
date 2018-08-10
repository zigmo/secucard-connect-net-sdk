using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General
{
    public class GeneralTransactionsService : ProductService<Transaction>
    {
        public static readonly ServiceMetaData<Transaction> MetaData = new ServiceMetaData<Transaction>("general",
            "transactions");

        protected override ServiceMetaData<Transaction> GetMetaData()
        {
            return MetaData;
        }
    }
}