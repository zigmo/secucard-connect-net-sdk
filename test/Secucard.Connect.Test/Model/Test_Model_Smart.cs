﻿/*
 * Copyright (c) 2015. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Secucard.Connect.Test.Model
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Smart.Model;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Model_Smart
    {
        [TestMethod, TestCategory("Model")]
        public void Test_Smart_Idents_1()
        {
            var json = File.ReadAllText("Data\\Model\\Smart.Idents.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Ident>>(json);
            Assert.AreEqual(data.List.Count, 3);
            var smartIdent = data.List.First();
            Assert.AreEqual(smartIdent.Id, "smi_1");
            Assert.AreEqual(smartIdent.Type, "card");
            Assert.AreEqual(smartIdent.Prefix, "9276");
            Assert.AreEqual(smartIdent.Length, 16);
            Assert.AreEqual(smartIdent.Name, "secucard Kundenkarte");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Smart_Transactions_1()
        {
            var json = File.ReadAllText("Data\\Model\\Smart.Transactions.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Transaction>>(json);
            Assert.IsTrue(data.List.Count > 1);
            var transaction = data.List.First();
            Assert.IsNotNull(transaction.Basket);
            Assert.AreEqual(transaction.FormattedCreated, "2015-07-09T14:18:41+02:00");
            Assert.AreEqual(transaction.FormattedUpdated, "2015-07-09T14:18:41+02:00");
            var product1 = data.List.First().Basket.Products.First();
            Assert.AreEqual(product1.ArticleNumber, "3378");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Smart_Devices_1()
        {
            var json = File.ReadAllText("Data\\Model\\Smart.Devices.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<Device>>(json);
            Assert.IsTrue(data.List.Count > 1);
            var device = data.List.First();
            Assert.AreEqual(device.Id, "SDV_99W7SUCEH2Y7TNQJB5GQG7HXWF7FAW");
            Assert.AreEqual(device.Merchant.Id, "MRC_2YPYFEYKF2DYG8Z4KHB5T8P2P4H0P6");
            Assert.AreEqual(device.Store.Id, "STO_WFMX9VS2VCC7TBBWWMP6HBAGP6PPPP");
            Assert.AreEqual(device.Type, "cashier");
            Assert.IsNotNull(device.GenralDevice);
            Assert.AreEqual(device.GenralDevice.Id, "DEV_2P2WSRMA63DRT4A9H2A8DDYBH00PPG");
            Assert.AreEqual(device.FormattedCreated, "2015-03-09T16:39:18+01:00");
            Assert.AreEqual(device.Online, false);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Smart_Routings_1()
        {
            var json = File.ReadAllText("Data\\Model\\Smart.Routings.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<List<Routing>>>(json);
            Assert.IsTrue(data.List.Count > 1);
        }
    }
}