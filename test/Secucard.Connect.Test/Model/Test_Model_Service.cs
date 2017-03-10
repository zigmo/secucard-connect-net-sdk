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
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.Service.Model.services;

    [TestClass]
    [DeploymentItem("Data\\Model", "Data\\Model")]
    public class Test_Model_Service
    {
        [TestMethod, TestCategory("Model")]
        public void Test_Service_Identrequest_1()
        {
            var json = File.ReadAllText("Data\\Model\\Services.Identrequest.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<IdentRequest>>(json);
            Assert.AreEqual(data.List.Count, 3);
            var identRequest = data.List.First();
            Assert.AreEqual(identRequest.Id, "SIR_24E46FTA92Y8GCHNR5GQG7PKRQUUAE");
            Assert.AreEqual(identRequest.Type, "person");
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Service_Identresults_1()
        {
            var json = File.ReadAllText("Data\\Model\\Services.Identresults.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<IdentResult>>(json);
            Assert.AreEqual(data.List.Count, 1);
            var identResult = data.List.First();
            Assert.AreEqual(identResult.Id, "SIS_WZPEW6AVX2YAJ5V6R5GQGJHXXMHBA7");
            Assert.IsTrue(identResult.Demo.Value);
        }

        [TestMethod, TestCategory("Model")]
        public void Test_Service_Identcontracts_1()
        {
            var json = File.ReadAllText("Data\\Model\\Services.Identcontracts.1.json");
            var data = JsonSerializer.DeserializeJson<ObjectList<IdentContract>>(json);
            Assert.AreEqual(data.List.Count, 1);
            var contract = data.List.First();
            Assert.AreEqual(contract.Id, "SIC_WNVNAA62B2Y7GEJVR5GQGW8EAN62A6");
            Assert.IsFalse(contract.Demo.Value);
        }
    }
}