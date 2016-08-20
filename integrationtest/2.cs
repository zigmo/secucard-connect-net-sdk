using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Secucard.Connect.Client;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest2
    {
        private string _errorMessage = string.Empty;
        private bool? _connected;

        [TestMethod]
        public void Test()
        {
            Util.SetStub(2);

            var client = Util.GetClient();
            client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;

            try
            {
                client.Open(new CancellationTokenSource().Token);
            }
            catch (AuthError ex)
            {
                _errorMessage = ex.Message;
            }

            // Check error message
            Assert.AreEqual("The client credentials are invalid", _errorMessage);

            // Client disconnected?
            Assert.IsTrue(_connected.HasValue && !_connected.Value);
        }

        private void ClientOnConnectionStateChangedEvent(object sender, ConnectionStateChangedEventArgs args)
        {
            _connected = args.Connected;
        }
    }
}
