using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest9
    {
        [TestMethod]
        public void Test()
        {
            Util.SetStub(9);

            var client = Util.GetClient();
            //client.Open(new CancellationTokenSource().Token);

            var checkins = client.Smart.Checkins.GetAll();
            Assert.AreEqual(2, checkins.Count);

            var checkin = checkins.First();
            // Check checkin values
            Assert.IsNull(checkin.Account);
            Assert.AreEqual(new DateTime(2000, 1, 1) + TimeZoneInfo.Local.BaseUtcOffset, checkin.Created);
            //Assert.AreEqual("John Doe", checkin.Customer.DisplayName);
            Assert.AreEqual("John", checkin.Customer.ForeName);
            Assert.AreEqual("Doe", checkin.Customer.SurName);
            Assert.AreEqual("Mr", checkin.Customer.Salutation);
            Assert.AreEqual("john@doe.com", checkin.Customer.Email);
            Assert.AreEqual("John Doe", checkin.CustomerName);
            Assert.AreEqual("CKI_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123", checkin.Id);
            Assert.AreEqual("smart.checkins", checkin.Object);
            Assert.AreEqual("MCD_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123", checkin.MerchantCard.Id);
            Assert.AreEqual("loyalty.merchantcards", checkin.MerchantCard.Object);
        }
    }
}
