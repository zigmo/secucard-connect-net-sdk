using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Smart.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart
{
    public class IdentsService : ProductService<Ident>
    {
        public static readonly ServiceMetaData<Ident> MetaData = new ServiceMetaData<Ident>("smart", "idents");

        protected override ServiceMetaData<Ident> GetMetaData()
        {
            return MetaData;
        }

        /// <summary>
        ///     Read an ident with a given id from a connected device.
        /// </summary>
        public Ident ReadIdent(string id)
        {
            return Execute<Ident>(id, "read", null, null, null);
        }
    }
}