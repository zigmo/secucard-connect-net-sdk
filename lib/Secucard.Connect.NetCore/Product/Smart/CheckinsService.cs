using System.Collections.Generic;
using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;
using Zigmo.Secucard.Connect.NetCore.Product.General.Model;
using Zigmo.Secucard.Connect.NetCore.Product.Smart.Event;
using Zigmo.Secucard.Connect.NetCore.Product.Smart.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Smart
{
    public class CheckinsService : ProductService<Checkin>
    {
        public CheckinEventHandler CheckinEvent;

        public static readonly ServiceMetaData<Checkin> MetaData = new ServiceMetaData<Checkin>("smart",
            "checkins");

        protected override ServiceMetaData<Checkin> GetMetaData()
        {
             return MetaData; 
        }

        public override void RegisterEvents()
        {
            Context.EventDispatcher.RegisterForEvent(GetType().Name, "smart.checkins", "changed", OnNewEvent);
        }

        private void OnNewEvent(object obj)
        {
            if (CheckinEvent != null)
                CheckinEvent(this, new CheckinEventEventArgs {SecucardEvent = (Event<Checkin>) obj});
        }

        public List<Checkin> GetAll()
        {
            var objectList = GetList(new QueryParams());
            ProcessCheckins(objectList.List);
            return objectList.List;
        }

        private static void ProcessCheckins(List<Checkin> checkins)
        {
            foreach (var checkin in checkins)
            {
                MediaResource picture = checkin.PictureObject;
                if (picture != null)
                {
                    if (!picture.IsCached)
                    {
                        picture.Download();
                    }
                }
            }
        }
    }
}