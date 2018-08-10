using Zigmo.Secucard.Connect.NetCore.Client;

namespace Zigmo.Secucard.Connect.NetCore.Auth.Exception
{
    /// <summary>
    /// Indicates an authentication was canceled by user request.
    /// </summary>
    public class AuthCanceledException : AuthError
    {
    }
}