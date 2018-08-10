namespace Zigmo.Secucard.Connect.NetCore.Auth
{
    public class AuthCanceledException : System.Exception
    {
        public AuthCanceledException(string message) : base(message)
        {
        }
    }
}