using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Secucard.Connect.Auth;
using Secucard.Connect.Auth.Exception;
using Secucard.Connect.Client;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest4
    {
        private string _errorMessage = string.Empty;
        private bool? _authPending;
        private bool? _authCanceledAfterPending;
        private bool? _connected;

        [TestMethod]
        public void Test()
        {
            Util.SetStub(4);

            var client = Util.GetClient();
            client.AuthEvent += ClientOnAuthEvent;
            client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;

            try
            {
                client.Open(new CancellationTokenSource().Token);
            }
            catch (AuthTimeoutException ex)
            {
                _errorMessage = ex.Message;
            }

            // Check error message
            Assert.AreEqual("Timeout signaled by timer", _errorMessage);

            // Client authentication canceled?
            Assert.IsTrue(_authCanceledAfterPending.HasValue && _authCanceledAfterPending.Value);

            // Client disconnected?
            Assert.IsTrue(_connected.HasValue && !_connected.Value);

            // Server validation?
            Assert.IsTrue(Util.ValidateStub());
        }

        private void ClientOnConnectionStateChangedEvent(object sender, ConnectionStateChangedEventArgs args)
        {
            _connected = args.Connected;
        }

        private void ClientOnAuthEvent(object sender, AuthEventArgs args)
        {
            switch (args.Status)
            {
                case AuthStatusEnum.Pending:
                    _authPending = true;
                    break;

                case AuthStatusEnum.Canceled:
                    if (_authPending.HasValue && _authPending.Value)
                    {
                        _authCanceledAfterPending = true;
                    }
                    break;
            }
        }
    }
}