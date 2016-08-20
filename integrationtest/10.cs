using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest10
    {
        [TestMethod]
        public void Test()
        {
            Util.SetStub(10);

            var client = Util.GetClient();
            client.Open(new CancellationTokenSource().Token);

            var idents = client.Smart.Idents.GetList(null);
            Assert.AreEqual(idents.Count, idents.List.Count);

            var ident = idents.List.First();
            // Check ident values
            Assert.IsNull(ident.Customer);
            Assert.AreEqual("smi_1", ident.Id);
            Assert.AreEqual(16, ident.Length);
            Assert.IsNull(ident.MerchantCard);
            Assert.AreEqual("secucard Kundenkarte", ident.Name);
            Assert.AreEqual("smart.idents", ident.Object);
            Assert.AreEqual("9276", ident.Prefix);
            Assert.AreEqual("card", ident.Type);
            Assert.IsNull(ident.Valid);
            Assert.IsNull(ident.Value);
        }
    }
}
