﻿using System;
using Zigmo.Secucard.Connect.NetCore.Auth.Model;
using Zigmo.Secucard.Connect.NetCore.Net.Rest;
using Zigmo.Secucard.Connect.NetCore.Net.Util;

namespace Zigmo.Secucard.Connect.NetCore.Auth
{
    public class RestAuth : RestBase
    {
        public const string Device = "device";
        public const string Refreshtoken = "refresh_token";
        public const string Clientcredentials = "client_credentials";

        private readonly string _host;

        public RestAuth(AuthConfig authConfig)
            : base(new RestConfig { Url = authConfig.OAuthUrl, ConnectTimeoutSec = authConfig.AuthWaitTimeoutSec })
        {
            _host = new Uri(authConfig.OAuthUrl).Host;
        }

        public string UserAgentInfo { get; set; }

        public DeviceAuthCode GetDeviceAuthCode(string clientId, string clientSecret, string uuid)
        {
            var req = new RestRequest
            {
                Host = _host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.GrantType, Device);
            req.BodyParameter.Add(AuthConst.ClientId, clientId);
            req.BodyParameter.Add(AuthConst.ClientSecret, clientSecret);
            req.BodyParameter.Add(AuthConst.Uuid, uuid);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<DeviceAuthCode>(ret);
        }

        public Token GetToken(string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = _host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.GrantType, Clientcredentials);
            req.BodyParameter.Add(AuthConst.ClientId, clientId);
            req.BodyParameter.Add(AuthConst.ClientSecret, clientSecret);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<Token>(ret);
        }

        public Token ObtainAuthToken(string deviceCode, string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = _host,
                UserAgent = UserAgentInfo
            };

            req.BodyParameter.Add(AuthConst.GrantType, Device);
            req.BodyParameter.Add(AuthConst.ClientId, clientId);
            req.BodyParameter.Add(AuthConst.ClientSecret, clientSecret);
            req.BodyParameter.Add(AuthConst.Code, deviceCode);

            try
            {
                var ret = RestPost(req);
                return JsonSerializer.DeserializeJson<Token>(ret);
            }
            catch (RestException ex)
            {
                // ignore 401 Error and return null
                if (ex.StatusCode == 401) return null;
                throw;
            }
        }

        public Token RefreshToken(string refreshToken, string clientId, string clientSecret)
        {
            var req = new RestRequest
            {
                Host = _host,
                UserAgent = UserAgentInfo
            };
            req.BodyParameter.Add(AuthConst.GrantType, Refreshtoken);
            req.BodyParameter.Add(AuthConst.ClientId, clientId);
            req.BodyParameter.Add(AuthConst.ClientSecret, clientSecret);
            req.BodyParameter.Add(AuthConst.RefreshToken, refreshToken);

            var ret = RestPost(req);
            return JsonSerializer.DeserializeJson<Token>(ret);
        }
    }
}