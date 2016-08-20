using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Secucard.Connect.Auth;
using Secucard.Connect.Auth.Model;
using Secucard.Connect.Client;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest1
    {
        private DeviceAuthCode _authCode;
        private bool? _authOk;
        private bool? _connected;

        [TestMethod]
        public void Test()
        {
            Util.SetStub(1);

            var client = Util.GetClient();
            client.AuthEvent += ClientOnAuthEvent;
            client.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;

            client.Open(new CancellationTokenSource().Token);

            // Check validation token values
            Assert.AreEqual("d1650a31e65426d1f40acc1b45a2cacb", _authCode.DeviceCode);
            Assert.AreEqual(1200, _authCode.ExpiresIn);
            Assert.AreEqual(5, _authCode.Interval);
            Assert.AreEqual("ffwuwqxu", _authCode.UserCode);
            Assert.AreEqual("http://127.0.0.1:5000/validate", _authCode.VerificationUrl);

            // Security token stored in Data Storage Interface?
            var token = (Token) Util.DataStorage.Get("token");
            Assert.IsNotNull(token);

            // Check security token values
            Assert.AreEqual("jmn2qjkn92fcikgihu3na53gf0", token.AccessToken);
            Assert.AreEqual(1200, token.ExpiresIn);
            Assert.AreEqual("aa0d46c18fa4f6e4597557da1f039002b6f87206", token.RefreshToken);
            Assert.AreEqual(null, token.Scope);
            Assert.AreEqual("bearer", token.TokenType);

            // Client authenticated?
            Assert.IsTrue(_authOk.HasValue && _authOk.Value);

            // Client connected?
            Assert.IsTrue(_connected.HasValue && _connected.Value);
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
                    _authCode = args.DeviceAuthCodes;
                    break;

                case AuthStatusEnum.Ok:
                    _authOk = true;
                    break;
            }
        }
    }
}
