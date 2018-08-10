using System.Collections.Generic;
using Zigmo.Secucard.Connect.NetCore.Auth;
using Zigmo.Secucard.Connect.NetCore.Event;
using Zigmo.Secucard.Connect.NetCore.Net;

namespace Zigmo.Secucard.Connect.NetCore.Client
{
    public class ClientContext
    {
        public ClientContext()
        {
            Channels = new Dictionary<string, Channel>();
            EventDispatcher = new EventDispatcher();
        }

        internal TokenManager TokenManager { get; set; }
        internal EventDispatcher EventDispatcher { get; set; }
        internal Dictionary<string, Channel> Channels { get; set; }
        public string DefaultChannel { get; set; }
        internal string AppId { get; set; }
    }
}