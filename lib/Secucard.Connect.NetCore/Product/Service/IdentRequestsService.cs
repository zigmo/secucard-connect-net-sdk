using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Service.Model.services;

namespace Zigmo.Secucard.Connect.NetCore.Product.Service
{
    /// <summary>
    /// Implements the services/identrequest operations.
    /// </summary>
    public class IdentRequestsService : ProductService<IdentRequest>
    {
        public static readonly ServiceMetaData<IdentRequest> MetaData = new ServiceMetaData<IdentRequest>("payment",
            "identrequests");

        protected override ServiceMetaData<IdentRequest> GetMetaData()
        {
            return MetaData;
        }
    }
}