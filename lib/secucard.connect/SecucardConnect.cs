﻿/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Trace;

    /// <summary>
    ///     Actual Client
    /// </summary>
    public class SecucardConnect
    {
        public const string VERSION = "0.1.development";
        private readonly ClientConfiguration Configuration;
        private readonly ClientContext Context;
        private readonly Dictionary<string, IService> Services;
        private volatile bool IsConnected;

        #region ### easy access services ### 

        public General General { get; set; }
        //public Document document;
        //public Payment payment;
        //public Loyalty loyalty;
        //public Services services;
        //public Smart smart;

        #endregion

        public event AuthEvent AuthEvent;
        public event ConnectionStateChangedEventHandler ConnectionStateChangedEvent;

        #region ### Open / Close ###

        /// <summary>
        ///    Authenticate and open internal resources
        /// </summary>
        public void Open()
        {
            if (IsConnected) return;

            try
            {
                var token = Context.TokenManager.GetToken(true);
                SecucardTrace.InfoSource("Auth successfull. Token = {0}", token);
            }
            catch (AuthError e)
            {
                Close();
                throw e;
            }

            try
            {
                foreach (var channel in Context.Channels.Values) channel.Open();
            }
            catch (Exception e)
            {
                Close();
                throw e;
            }

            IsConnected = true;
            OnConnectionStateChangedEvent(new ConnectionStateChangedEventArgs {Connected = IsConnected});

            SecucardTrace.Info("Secucard connect client opened.");
        }

        /// <summary>
        ///    Gracefully closes this instance and releases all resources.
        /// </summary>
        public void Close()
        {
            IsConnected = false;
            try
            {
                foreach (var channel in Context.Channels.Values)
                {
                    channel.Close();
                }
            }
            catch (Exception e)
            {
                SecucardTrace.Info(e.ToString());
            }

            OnConnectionStateChangedEvent(new ConnectionStateChangedEventArgs {Connected = IsConnected});

            SecucardTrace.Info("Secucard connect client closed.");
        }

        #endregion

        #region ### Events ###

        private void TokenManagerOnTokenManagerStatusUpdateEvent(object sender, TokenManagerStatusUpdateEventArgs args)
        {
            // Send Events vom Auth Provider an pass it up to client
            OnAuthEvent(new AuthEventArgs {Status = args.Status, DeviceAuthCodes = args.DeviceAuthCodes});
        }

        private void OnAuthEvent(AuthEventArgs args)
        {
            if (AuthEvent != null) AuthEvent(this, args);
        }

        private void OnConnectionStateChangedEvent(ConnectionStateChangedEventArgs args)
        {
            if (ConnectionStateChangedEvent != null) ConnectionStateChangedEvent(this, args);
        }

        private void HandelChannelEvents()
        {
            // Raise connection state changed on client
            // Raise channel events to interested services.
        }

        #endregion

        #region ### Factory Client ###

        private SecucardConnect(ClientConfiguration configuration)
        {
            Configuration = configuration;

            // Setup Trace if directory is there
            if (!string.IsNullOrWhiteSpace(configuration.TraceDir))
            {
                var listener = new SecucardTraceListener(Configuration.TraceDir) { Name = "SecucardTraceListener" };
                System.Diagnostics.Trace.Listeners.Add(listener);
                SecucardTrace.EmptyLine();
            }

            var context = new ClientContext
            {
                AppId = Configuration.AppId,
            };
            // context.DataStorage = dataStorage;

            Context = context;

            var authConfig = Configuration.AuthConfig;
            var stompConfig = Configuration.StompConfig;
            var restConfig = Configuration.RestConfig;

            SecucardTrace.Info(string.Format("Creating client with configuration: {0}", configuration));

            if (Configuration.ClientAuthDetails == null)
            {
                //TODO:
            }

            context.DefaultChannel = Configuration.DefaultChannel;

            var restChannel = new RestChannel(restConfig, context);
            context.Channels.Add(ChannelOptions.CHANNEL_REST, restChannel);


            if (Configuration.StompEnabled)
            {
                var stompChannel = new StompChannel(stompConfig, context);
                context.Channels.Add(ChannelOptions.CHANNEL_STOMP, stompChannel);
                // TODO: Listen to Events stompChannel.
            }

            // TODO: Setup Event listener for channels
            // TODO: Setup Event dispatcher for incoming events from channels

            var restAuth = new RestAuth(authConfig)
            {
                UserAgentInfo =
                    "secucardconnect-net-" + VERSION + "/net:" + Environment.OSVersion + " " + Environment.Version
            };
            context.TokenManager = new TokenManager(authConfig, Configuration.ClientAuthDetails, restAuth);
            context.TokenManager.TokenManagerStatusUpdateEvent += TokenManagerOnTokenManagerStatusUpdateEvent;

            Services = ServiceFactory.CreateServices(context);
            WireServiceInstances();
        }

        public static SecucardConnect Create(ClientConfiguration configuration)
        {
            if (configuration == null)
                configuration = ClientConfiguration.Get();

            if (configuration.DataStorage == null)
                throw new Exception("Missing cache implementation found in config.");

            var client = new SecucardConnect(configuration);

            return client;
        }

        #endregion

        #region ### Services ###

        public T GetService<T>()
        {
            return (T) Services[typeof (T).Name];
        }

        private void WireServiceInstances()
        {
            //document = new Document(service(Document.Uploads));

            General = new General
            {
                News = GetService<NewsService>(),
                Accountdevices = GetService<AccountDevicesService>(),
                Accounts = GetService<AccountsService>(),
                Merchants = GetService<MerchantsService>(),
                Publicmerchants = GetService<PublicMerchantsService>(),
                Stores = GetService<StoresService>(),
                Transactions = GetService<TransactionsService>()
            };


            //payment = new Payment(service(Payment.Containers), service(Payment.Customers), service(Payment.Secupaydebits),
            //    service(Payment.Secupayprepays), service(Payment.Contracts));

            //loyalty = new Loyalty(service(Loyalty.Cards), service(Loyalty.Customers), service(Loyalty.Merchantcards));

            //services = new Services(service(Services.Identrequests), service(Services.Identresults));

            //smart = new Smart(service(Smart.Checkins), service(Smart.Idents), service(Smart.Transactions));
        }

        #endregion
    }
}