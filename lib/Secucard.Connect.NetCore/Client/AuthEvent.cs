using Zigmo.Secucard.Connect.NetCore.Auth;
using Zigmo.Secucard.Connect.NetCore.Auth.Model;

namespace Zigmo.Secucard.Connect.NetCore.Client
{
    using DeviceAuthCode = DeviceAuthCode;

    public delegate void AuthEvent(object sender, AuthEventArgs args);

    public class AuthEventArgs
    {
        public DeviceAuthCode DeviceAuthCodes { get; set; }
        public AuthStatusEnum Status { get; set; }
    }
}