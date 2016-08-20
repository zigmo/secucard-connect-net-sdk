using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Secucard.Connect.IntegrationTest
{
    [TestClass]
    public class IntegrationTest11
    {
        [TestMethod]
        public void Test()
        {
            Util.SetStub(11);

            var client = Util.GetClient();
            client.Open(new CancellationTokenSource().Token);

            var transactions = client.Smart.Transactions.GetList(null);

            Assert.AreEqual(1, transactions.Count);

            var transaction = transactions.List.First();

            // Check transaction
            Assert.AreEqual("smart.transactions", transaction.Object);
            Assert.AreEqual("STX_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123", transaction.Id);
            Assert.AreEqual(new DateTime(2016, 1, 1) + TimeZoneInfo.Local.BaseUtcOffset, transaction.Created);
            Assert.AreEqual(new DateTime(2016, 1, 1, 1, 0, 0) + TimeZoneInfo.Local.BaseUtcOffset, transaction.Updated);
            Assert.AreEqual("created", transaction.Status);
            Assert.IsNull(transaction.TransactionRef);
            Assert.IsNull(transaction.MerchantRef);
            Assert.AreEqual(2, transaction.Basket.Products.Count);
            Assert.AreEqual("EUR", transaction.BasketInfo.Currency);
            Assert.AreEqual(9876, transaction.BasketInfo.Sum);

            // Check basket product 1
            var product = transaction.Basket.Products[0];
            Assert.AreEqual(0, product.Id);
            Assert.AreEqual("12345", product.ArticleNumber);
            Assert.AreEqual("4006381333931", product.Ean);
            Assert.AreEqual("¼½¾ÀÁÂÃÄÅÆÇßŌōŎŏŐő", product.Desc);
            Assert.AreEqual(5, product.Quantity);
            Assert.AreEqual(17, product.PriceOne);
            Assert.AreEqual(3, product.Tax);

            // Check basket product 2
            product = transaction.Basket.Products[1];
            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("23456", product.ArticleNumber);
            Assert.AreEqual("12345678", product.Ean);
            Assert.AreEqual("ŕŜšŸŽǄabcdefg12345", product.Desc);
            Assert.AreEqual(1, product.Quantity);
            Assert.AreEqual(5678, product.PriceOne);
            Assert.AreEqual(7, product.Tax);

            // Check ident
            Assert.AreEqual(1, transaction.Idents.Count);

            var ident = transaction.Idents[0];
            Assert.AreEqual("smart.idents", ident.Object);
            Assert.AreEqual("smi_1", ident.Id);
            Assert.AreEqual("9276", ident.Prefix);
            Assert.AreEqual("secucard Kundenkarte", ident.Name);
            Assert.AreEqual("card", ident.Type);
            Assert.AreEqual("9276012345678900", ident.Value);
            Assert.IsTrue(ident.Valid.HasValue && ident.Valid.Value);
            Assert.AreEqual("loyalty.merchantcards", ident.MerchantCard.Object);
            Assert.AreEqual("general.merchants", ident.MerchantCard.Merchant.Object);
            Assert.AreEqual("MRC_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123", ident.MerchantCard.Merchant.Id);
            Assert.AreEqual("general.merchants", ident.MerchantCard.CreatedForMerchant.Object);
            Assert.AreEqual("MRC_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123", ident.MerchantCard.CreatedForMerchant.Id);
            Assert.AreEqual("general.stores", ident.MerchantCard.CreatedForStore.Object);
            Assert.AreEqual("STO_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123", ident.MerchantCard.CreatedForStore.Id);
            Assert.AreEqual("loyalty.cards", ident.MerchantCard.Card.Object);
            Assert.AreEqual("CRD_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123", ident.MerchantCard.Card.Id);
            Assert.AreEqual("9276012345678900", ident.MerchantCard.Card.CardNumber);
            Assert.AreEqual(new DateTime(2000, 1, 1, 7, 0, 0) + TimeZoneInfo.Local.BaseUtcOffset,
                ident.MerchantCard.Card.Created);
            Assert.IsTrue(ident.MerchantCard.IsBaseCard);
            Assert.AreEqual("loyalty.cardgroups", ident.MerchantCard.Cardgroup.Object);
            Assert.AreEqual("CRG_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123", ident.MerchantCard.Cardgroup.Id);
            Assert.AreEqual("Display name", ident.MerchantCard.Cardgroup.DisplayName);
            Assert.AreEqual("Display name raw", ident.MerchantCard.Cardgroup.DisplayNameRaw);
            Assert.AreEqual(0, ident.MerchantCard.Cardgroup.StockWarnLimit);
            Assert.IsTrue(Util.GetStaticFile("11.jpg").SequenceEqual(ident.MerchantCard.Cardgroup.PictureObject.GetContents()));
            Assert.AreEqual(1234, ident.MerchantCard.Balance);
            Assert.AreEqual(0, ident.MerchantCard.Points);
            Assert.AreEqual(1234, ident.MerchantCard.Balance);
            Assert.AreEqual(new DateTime(2016, 1, 1, 12, 0, 0) + TimeZoneInfo.Local.BaseUtcOffset,
                ident.MerchantCard.LastUsage);
            Assert.AreEqual(new DateTime(2016, 1, 1, 12, 0, 0) + TimeZoneInfo.Local.BaseUtcOffset,
                ident.MerchantCard.LastCharge);
            Assert.AreEqual("active", ident.MerchantCard.StockStatus);
            Assert.AreEqual("unlocked", ident.MerchantCard.LockStatus);
        }
    }
}
