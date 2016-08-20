using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Secucard.Connect.Auth;
using Secucard.Connect.Auth.Model;
using Secucard.Connect.Client;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest7
    {
        private string _errorMessage = string.Empty;
        private bool? _authCanceled;

        [TestMethod]
        public void Test()
        {
            Util.SetStub(7);

            var client = Util.GetClient();
            client.AuthEvent += ClientOnAuthEvent;

            var cancellationToken = new CancellationTokenSource();
            cancellationToken.CancelAfter(1000);

            try
            {
                client.Open(cancellationToken.Token);
            }
            catch (AuthCanceledException ex)
            {
                _errorMessage = ex.Message;
            }

            // Check error message
            Assert.AreEqual("Authorization canceled by request.", _errorMessage);

            // Security token stored in Data Storage Interface?
            var token = (Token)Util.DataStorage.Get("token");
            Assert.IsNull(token);

            // Authentication canceled?
            Assert.IsTrue(_authCanceled.HasValue && _authCanceled.Value);
        }

        private void ClientOnAuthEvent(object sender, AuthEventArgs args)
        {
            if (args.Status == AuthStatusEnum.Canceled)
            {
                _authCanceled = true;
            }
        }
    }
}
