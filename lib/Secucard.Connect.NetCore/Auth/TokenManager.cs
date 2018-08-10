﻿using System;
using System.Threading;
using Zigmo.Secucard.Connect.NetCore.Auth.Exception;
using Zigmo.Secucard.Connect.NetCore.Auth.Model;
using Zigmo.Secucard.Connect.NetCore.Client;

namespace Zigmo.Secucard.Connect.NetCore.Auth
{
    /// <summary>
    ///     Implementation of the TokenManager which gets an OAuth token via REST.
    ///     The retrieved token is also cached and refreshed.
    /// </summary>
    public class TokenManager
    {
        private readonly AuthConfig _config;
        private readonly RestAuth _rest;

        public TokenManager(AuthConfig config, IClientAuthDetails clientAuthDetails, RestAuth restAuth)
        {
            _config = config;
            ClientAuthDetails = clientAuthDetails;
            _rest = restAuth;
        }

        private bool CancelAuthFlag { get; set; }
        private IClientAuthDetails ClientAuthDetails { get; set; }
        public event TokenManagerStatusUpdateEventHandler TokenManagerStatusUpdateEvent;

        private Token GetCurrent()
        {
            if (ClientAuthDetails != null)
            {
                return ClientAuthDetails.GetCurrent();
            }
            return null;
        }

        public string GetToken(bool allowInteractive)
        {
            var token = GetCurrent();

            var authenticate = false;

            if (token == null)
            {
                // no token, authenticate first
                authenticate = true;
            }
            else if (token.IsExpired())
            {
                // try refresh if just expired, authenticate new if no refresh possible or failed
                SecucardTrace.InfoSource("Token expired: {0} , original:{1}",
                    token.ExpireTime?.ToString() ?? "null",
                    token.OrigExpireTime == null ? "null" : token.OrigExpireTime.ToString());
                if (token.RefreshToken == null)
                {
                    SecucardTrace.Info("No token refresh possible, try obtain new.");
                    authenticate = true;
                }
                else
                {
                    try
                    {
                        Refresh(token, ClientAuthDetails.GetClientCredentials());
                        SetCurrentToken(token);
                    }
                    catch (System.Exception ex)
                    {
                        SecucardTrace.Info("Token refresh failed, try obtain new. {0}", ex);
                        authenticate = true;
                    }
                }
            }
            else
            {
                // we should have valid token in cache, no new auth necessary
                if (_config.ExtendExpire)
                {
                    SecucardTrace.Info("Extend token expire time.");
                    token.SetExpireTime();
                    SetCurrentToken(token);
                }
                SecucardTrace.Info("Return current token: {0}", token);
            }

            if (authenticate)
            {
                var credentials = ClientAuthDetails.GetCredentials();

                if (credentials is AnonymousCredentials)
                {
                    return null;
                }

                // new authentication is needed but only if allowed
                if ((credentials is AppUserCredentials || credentials is DeviceCredentials) && !allowInteractive)
                {
                    throw new AuthFailedException("Invalid access token, please authenticate again.");
                }

                token = Authenticate(credentials);
                token.SetExpireTime();
                token.Id = credentials.Id;
                SetCurrentToken(token);
                SecucardTrace.Info("Return new token: {0}", token);

                OnTokenManagerStatusUpdateEvent(new TokenManagerStatusUpdateEventArgs
                {
                    Status = AuthStatusEnum.Ok,
                    Token = token
                });
            }

            return token.AccessToken;
        }

        private void Refresh(Token token, ClientCredentials credentials)
        {
            if (credentials == null)
            {
                throw new System.Exception("Missing credentials");
            }

            SecucardTrace.Info("Refresh token: {0}", credentials);
            var refreshToken = _rest.RefreshToken(token.RefreshToken, credentials.ClientId, credentials.ClientSecret);
            token.AccessToken = refreshToken.AccessToken;
            token.ExpiresIn = refreshToken.ExpiresIn;
            if (!string.IsNullOrWhiteSpace(refreshToken.RefreshToken)) token.RefreshToken = refreshToken.RefreshToken;
            token.SetExpireTime();
        }

        private Token Authenticate(OAuthCredentials credentials)
        {
            if (credentials == null)
            {
                throw new AuthFailedException("Missing credentials");
            }

            SecucardTrace.Info("Authenticate credentials: {0}", credentials.ToString());

            var pollInterval = 0;
            var timeout = DateTime.Now;
            var devicesCredentials = credentials as DeviceCredentials;
            var isDeviceAuth = (devicesCredentials != null);
            DeviceAuthCode codes = null;


            // if DeviceAuth then get codes an pass to app thru event. Further action required by client
            if (isDeviceAuth)
            {
                codes = _rest.GetDeviceAuthCode(devicesCredentials.ClientId, devicesCredentials.ClientSecret, devicesCredentials.DeviceId);
                if (TokenManagerStatusUpdateEvent != null)
                    TokenManagerStatusUpdateEvent.Invoke(this,
                        new TokenManagerStatusUpdateEventArgs
                        {
                            DeviceAuthCodes = codes,
                            Status = AuthStatusEnum.Pending
                        });

                SecucardTrace.Info("Retrieved codes for device auth: {0}, now polling for auth.", codes);

                // set poll timeout, either by config or by expire time of code
                var t = codes.ExpiresIn;
                if (t <= 0 || _config.AuthWaitTimeoutSec < t)
                {
                    t = _config.AuthWaitTimeoutSec;
                }
                timeout = DateTime.Now.AddSeconds(t*1000);

                pollInterval = codes.Interval;
                if (pollInterval <= 0)
                {
                    pollInterval = 5; // poll default 5s
                }

                devicesCredentials.DeviceCode = codes.DeviceCode;
                devicesCredentials.DeviceId = null; // device id must be empty for next auth. step!
            }


            do
            {
                Token token = null;
                if (isDeviceAuth)
                {
                    // in case of device auth, check for cancel and delay polling
                    if (CancelAuthFlag) throw new AuthCanceledException("Authorization canceled by request.");
                    Thread.Sleep(pollInterval*1000);

                    token = _rest.ObtainAuthToken(codes.DeviceCode, devicesCredentials.ClientId,
                        devicesCredentials.ClientSecret);
                    if (token == null) // auth not completed yet
                    {
                        OnTokenManagerStatusUpdateEvent(new TokenManagerStatusUpdateEventArgs
                        {
                            DeviceAuthCodes = codes,
                            Status = AuthStatusEnum.Pending
                        });
                    }
                }
                else
                {
                    var clientCredentials = credentials as ClientCredentials;
                    if (clientCredentials != null)
                        token = _rest.GetToken(clientCredentials.ClientId, clientCredentials.ClientSecret);
                }

                if (token != null)
                {
                    return token;
                }
            } while (DateTime.Now < timeout);

            if (isDeviceAuth)
            {
                throw new AuthTimeoutException();
            }

            throw new System.Exception("Unexpected failure of authentication.");
        }

        private void OnTokenManagerStatusUpdateEvent(TokenManagerStatusUpdateEventArgs args)
        {
            if (TokenManagerStatusUpdateEvent != null) TokenManagerStatusUpdateEvent.Invoke(this, args);
        }

        private void SetCurrentToken(Token token)
        {
            if (ClientAuthDetails != null) ClientAuthDetails.OnTokenChanged(token);
        }
    }
}