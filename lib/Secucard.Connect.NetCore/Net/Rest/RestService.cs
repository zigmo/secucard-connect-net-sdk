using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zigmo.Secucard.Connect.NetCore.Net.Util;
using Zigmo.Secucard.Connect.NetCore.Product.Common.Model;

namespace Zigmo.Secucard.Connect.NetCore.Net.Rest
{
    public class RestService : RestBase
    {
        public RestService(RestConfig restConfig)
            : base(restConfig)
        {
        }

        public ObjectList<T> GetList<T>(RestRequest request)
        {
            var ret = RestGet(request);

            return JsonSerializer.DeserializeJson<ObjectList<T>>(ret);
        }

        public T GetObject<T>(RestRequest request)
        {
            var ret = RestGet(request);

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public T PostObject<T>(RestRequest request)
        {
            request.BodyJsonString = JsonSerializer.SerializeJson((T) request.Object);
            var ret = RestPost(request);

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public List<T> PostObjectList<T>(RestRequest request)
        {
            request.BodyJsonString = JsonSerializer.SerializeJsonList(request.Objects.Cast<T>().ToList());
            var ret = RestPost(request);

            return JsonSerializer.DeserializeJsonList<T>(ret);
        }

        public T PutObject<T>(RestRequest request)
        {
            request.BodyJsonString = JsonSerializer.SerializeJson((T) request.Object);
            var ret = RestPut(request);

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public void DeleteObject(RestRequest request)
        {
            RestDelete(request);
        }

        public T Execute<T>(RestRequest request)
        {
            request.BodyJsonString = JsonSerializer.SerializeJson(request.Object);
            var ret = RestExecute(request);

            return JsonSerializer.DeserializeJson<T>(ret);
        }

        public Stream GetStream(RestRequest request)
        {
            return RestGetStream(request);
        }
    }
}