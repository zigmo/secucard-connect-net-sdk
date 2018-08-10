using Zigmo.Secucard.Connect.NetCore.Auth.Model;
using Zigmo.Secucard.Connect.NetCore.Storage;

namespace Zigmo.Secucard.Connect.NetCore.Auth
{
    /// <summary>
    /// Abstract implementation which just delegates the token persistence to a memory based cache.
    /// </summary>
    public abstract class AbstractClientAuthDetails
    {
        protected readonly DataStorage _storage;

        public AbstractClientAuthDetails()
        {
            _storage = new MemoryDataStorage();
        }

        public Token GetCurrent()
        {
            return (Token) _storage.Get("token");
        }

        public void OnTokenChanged(Token token)
        {
            _storage.Save("token", token);
        }
    }
}