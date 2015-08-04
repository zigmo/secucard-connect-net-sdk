﻿namespace Secucard.Connect
{
    using System;
    using System.Collections.Generic;
    using Secucard.Connect.auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Product;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using Secucard.Model;
    using Secucard.Stomp;

    /// <summary>
    /// Actual Client
    /// </summary>
    public class SecucardConnect
    {
        public string Version { get { return "0.1.development"; } }

        public bool IsConnected { get; set; }

        public event SecucardConnectEvent SecucardConnectEvent;

        private ClientContext Context;

        private Dictionary<string, IService> Services; 

        #region ### Start / Stop ###

        public void Connect()
        {
            // Start authentification
            Context.AuthProvider.AuthProviderStatusUpdate += AuthProviderOnAuthProviderStatusUpdate;
            Context.AuthProvider.GetToken(false);

            //TODO: Start Stomp

            IsConnected = true;

            // TODO:Fire Event Connected
        }

        public void CancelAuth()
        {
        }

        public void Disconnect()
        {
           //TODO: Teardown
        }

        private void AuthProviderOnAuthProviderStatusUpdate(object sender, AuthProviderStatusUpdateEventArgs args)
        {
            // Send Events vom Auth Provider 
            if(SecucardConnectEvent!=null) 
                SecucardConnectEvent.Invoke(this,new SecucardConnectEventArgs {Status = args.Status,DeviceAuthCodes = args.DeviceAuthCodes});
        }

        #endregion

        #region ### Factory Client ###

        private SecucardConnect(ClientContext context)
        {
            this.Context = context;
            Services = ServiceFactory.CreateServices(context);

        }


        public static SecucardConnect Create(string id, ClientConfiguration config, DataStorage dataStorage, ISecucardTrace secucardTrace)
        {
            // Factory
            ClientContext context = new ClientContext(id, config, dataStorage, secucardTrace);


            return new SecucardConnect(context);
        }

        #endregion

        #region ### Factory Service ###

        public T GetService<T>()
        {
            return (T)Services[typeof (T).Name];
        }

        #endregion


    }


}
