using System.Collections.Generic;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Net
{
    public class ChannelRequest
    {
        public ChannelRequest()
        {
            ActionArgs = new List<string>();
        }

        public ChannelMethod Method { get; set; }
        public string Product { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public List<string> ActionArgs { get; set; }

        public string ObjectId { get; set; }
        public QueryParams QueryParams { get; set; }

        public object Object { get; set; }
        public List<SecuObject> Objects { get; set; }
        public string AppId { get; set; }

        public ChannelOptions ChannelOptions { get; set; }
    }
}