﻿namespace Secucard.Model
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public static class JsonSerializer
    {
        public static T DeserializeJson<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof (T));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                return (T) serializer.ReadObject(ms);
            }
        }

        public static string SerializeJson<T>(T data)
        {
            var serializer = new DataContractJsonSerializer(typeof (T));
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}