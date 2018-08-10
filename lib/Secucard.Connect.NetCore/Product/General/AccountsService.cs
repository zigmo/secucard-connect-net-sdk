using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.General
{
    public class AccountsService : ProductService<Account>
    {
        public static readonly ServiceMetaData<Account> MetaData = new ServiceMetaData<Account>("general", "accounts");

        protected override ServiceMetaData<Account> GetMetaData()
        {
            return MetaData;
        }
    }
}