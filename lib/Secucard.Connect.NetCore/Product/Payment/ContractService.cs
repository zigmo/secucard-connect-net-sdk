using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Payment.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    public class ContractService : ProductService<Contract>
    {
        public static readonly ServiceMetaData<Contract> MetaData = new ServiceMetaData<Contract>("payment", "contracts");

        public Contract CloneMyContract(CloneParams cloneParams)
        {
            return this.Execute<Contract>("me", "clone", null, cloneParams, null);
        }

        public Contract Clone(string contractId, CloneParams cloneParams)
        {
            return this.Execute<Contract>(contractId, "clone", null, cloneParams, null);
        }

        protected override ServiceMetaData<Contract> GetMetaData()
        {
            return MetaData;
        }
    }
}