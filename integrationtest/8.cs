using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Secucard.Connect.Auth.Model;
using Secucard.Connect.Client;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest8
    {
        private bool? _connected;

        [TestMethod]
        public void Test()
        {
            Util.SetStub(8);

            var client = Util.GetClient();
            client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;

            client.Open(new CancellationTokenSource().Token);
            client.Close();

            // Security token stored in Data Storage Interface?
            var token = (Token)Util.DataStorage.Get("token");
            Assert.IsNotNull(token);

            // Check security token values
            Assert.AreEqual("jmn2qjkn92fcikgihu3na53gf0", token.AccessToken);
            Assert.AreEqual(1200, token.ExpiresIn);
            Assert.AreEqual("aa0d46c18fa4f6e4597557da1f039002b6f87206", token.RefreshToken);
            Assert.AreEqual(null, token.Scope);
            Assert.AreEqual("bearer", token.TokenType);

            // Client connected?
            Assert.IsTrue(_connected.HasValue && !_connected.Value);
        }

        private void ClientOnConnectionStateChangedEvent(object sender, ConnectionStateChangedEventArgs args)
        {
            _connected = args.Connected;
        }
    }
}
