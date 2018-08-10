using Zigmo.Secucard.Connect.NetCore.Client;

namespace Zigmo.Secucard.Connect.NetCore.Auth.Exception
{
    /// <summary>
    /// Indicates an authentication was aborted due a timeout.
    /// </summary>
    public class AuthTimeoutException : AuthError
    {
    }
}