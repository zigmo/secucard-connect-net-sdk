using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Secucard.Connect.Client.Config;
using Secucard.Connect.Storage;
using System.Runtime.Serialization.Json;

namespace Secucard.Connect.IntegrationTest
{
    [DataContract]
    class Validation
    {
        [DataMember(Name = "valid", IsRequired = true)]
        public bool Valid { get; set; }

        [DataMember(Name="message")]
        public string Message { get; set; }
    }

    class Util
    {
        public static byte[] GetStaticFile(string filename)
        {
            MemoryStream ms = new MemoryStream();
            WebRequest.Create(Settings.Default.StubServer + Settings.Default.Static + filename)
                .GetResponse()
                .GetResponseStream().CopyTo(ms);

            return ms.ToArray();
        }


        public static void SetStub(int stubId)
        {
            WebRequest.Create(Settings.Default.StubServer + Settings.Default.SetStub + stubId).GetResponse();
            var response = WebRequest.Create(Settings.Default.StubServer + Settings.Default.GetStub).GetResponse().GetResponseStream();
            var streamReader = new StreamReader(response);

            bool stubSet = false;
            int id;
            if(int.TryParse(streamReader.ReadLine(), out id))
            {
                stubSet = (id == stubId);
            }

            if(!stubSet)
            {
                throw new Exception("Cannot set stub id");
            }
        }

        public static bool ValidateStub()
        {
            var response = WebRequest.Create(Settings.Default.StubServer + Settings.Default.ValidateStub)
                .GetResponse();
                
            var serializer = new DataContractJsonSerializer(typeof(Validation));
            var validation = serializer.ReadObject(response.GetResponseStream()) as Validation;
            return validation.Valid;
        }

        public static MemoryDataStorage DataStorage;

        public static SecucardConnect GetClient()
        {
            DataStorage = new MemoryDataStorage();
            
            // Load default properties
            var properties = Properties.Load(Path.Combine(Environment.CurrentDirectory, "SecucardConnect.config"));

            // Perpare client config. Implement your own Auth Details
            var clientConfiguration = new ClientConfiguration(properties)
            {
                ClientAuthDetails = new ClientAuthDetails(DataStorage),
                DataStorage = DataStorage
            };

            // Create client and attach client event handlers
            return SecucardConnect.Create(clientConfiguration);
        }
    }
}
