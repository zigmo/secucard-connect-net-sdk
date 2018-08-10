using System;
using Zigmo.Secucard.Connect.NetCore.Auth.Model;

namespace Zigmo.Secucard.Connect.NetCore.Auth
{
    public delegate void TokenManagerStatusUpdateEventHandler(object sender, TokenManagerStatusUpdateEventArgs args);

    public class TokenManagerStatusUpdateEventArgs : EventArgs
    {
        public DeviceAuthCode DeviceAuthCodes { get; set; }
        public AuthStatusEnum Status { get; set; }
        public Token Token { get; set; }
    }
}