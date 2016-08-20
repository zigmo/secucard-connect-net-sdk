using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Secucard.Connect.Auth.Model;
using Secucard.Connect.Client;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest6
    {
        private bool? _connected;

        [TestMethod]
        public void Test()
        {
            Util.SetStub(6);

            var client = Util.GetClient();
            client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;

            // Set valid token
            var validToken = new Token {ExpiresIn = 1200, AccessToken = "Test"};
            validToken.SetExpireTime();
            Util.DataStorage.Save("token", validToken);

            client.Open(new CancellationTokenSource().Token);

            // Token not refreshed?
            var token = Util.DataStorage.Get("token") as Token;
            Assert.AreEqual(validToken.AccessToken, token.AccessToken);

            // Client connected?
            Assert.IsTrue(_connected.HasValue && _connected.Value);

            // Server validation?
            Assert.IsTrue(Util.ValidateStub());
        }

        private void ClientOnConnectionStateChangedEvent(object sender, ConnectionStateChangedEventArgs args)
        {
            _connected = args.Connected;
        }
    }
}
