using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secucard.Connect.Auth;
using Secucard.Connect.Auth.Model;
using Secucard.Connect.Storage;

namespace Secucard.Connect.IntegrationTest
{
    class ClientAuthDetails : AbstractClientAuthDetails, IClientAuthDetails
    {
        public ClientAuthDetails(MemoryDataStorage dataStorage)
            : base(dataStorage)
        {
        }

        public OAuthCredentials GetCredentials()
        {
            return new DeviceCredentials("611c00ec6b2be6c77c2338774f50040b",
                "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb",
                "/vendor/unknown/cashier/dotnettest1");
        }

        public ClientCredentials GetClientCredentials()
        {
            return (ClientCredentials)GetCredentials();
        }
    }
}
