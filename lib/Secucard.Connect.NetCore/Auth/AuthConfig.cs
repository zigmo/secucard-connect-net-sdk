﻿using Zigmo.Secucard.Connect.NetCore.Client.Config;

namespace Zigmo.Secucard.Connect.NetCore.Auth
{
    public class AuthConfig
    {
        public string OAuthUrl { get; set; }
        public int AuthWaitTimeoutSec { get; set; }
        public bool ExtendExpire { get; set; }

        public AuthConfig(Properties properties)
        {
            OAuthUrl = properties.Get("Auth.OAuthUrl", "https://connect.secucard.com/oauth/token");
            AuthWaitTimeoutSec = properties.Get("Auth.AuthWaitTimeoutSec", 240);
            ExtendExpire = properties.Get("Auth.ExtendExpire", true);
        }
    }
}