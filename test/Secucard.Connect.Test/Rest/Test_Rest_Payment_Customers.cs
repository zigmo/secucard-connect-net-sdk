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

namespace Secucard.Connect.Test.Rest
{
    using System;
    using System.Linq;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;
    using Secucard.Connect.Product.Payment.Model;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Rest_Payment_Customers : Test_Rest_Base_AuthUser
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Payment_Customers_1_GET()
        {
            var request = new RestRequest
            {
                Token = Token,
                QueryParams = new QueryParams
                {
                    Count = 10,
                    Offset = 0
                },
                PageUrl = "Payment/Customers",
                Host = "core-dev10.secupay-ag.de"
            };

            var data = RestService.GetList<Customer>(request);

            Assert.IsTrue(data.Count > 0);
        }

        [TestMethod, TestCategory("Rest")]
        public void Test_Payment_Customers_2_POST_PUT_GET_DEL()
        {
            var customer = new Customer
            {
                Contact = new Contact
                {
                    Salutation = "Herr",
                    Title = "Dr.",
                    Forename = "Forename-" + DateTime.Now.Ticks,
                    Surname = "Surname-" + DateTime.Now.Ticks,
                    CompanyName = "Company-" + DateTime.Now.Ticks,
                    DateOfBirth = new DateTime(1970, 1, 1),
                    Email = "test@hutzlibu.com",
                    Phone = "0049-987-654321",
                    Mobile = "0049-170-654321",
                    Address = new Address
                    {
                        Street = "Hauptstrasse",
                        StreetNumber = "23a",
                        PostalCode = "01234",
                        City = "Entenhausen",
                        Country = "Germany"
                    }
                }
            };

            // POST
            Customer customerPost;
            {
                var request = new RestRequest
                {
                    Token = Token,
                    Object = customer,
                    PageUrl = "Payment/Customers",
                    Host = "core-dev10.secupay-ag.de"
                };

                customerPost = RestService.PostObject<Customer>(request);

                Assert.AreEqual(customerPost.Object, "payment.customers");
                Assert.IsNotNull(customerPost.Contract);
                Assert.IsNotNull(customerPost.Contact);
                Assert.IsTrue(customerPost.Created > DateTime.Now.AddMinutes(-2));
            }

            // GET
            {
                var request = new RestRequest
                {
                    Token = Token,
                    QueryParams = new QueryParams {Query = "id:" + customerPost.Id},
                    PageUrl = "Payment/Customers",
                    Host = "core-dev10.secupay-ag.de"
                };

                var data = RestService.GetList<Customer>(request);

                Assert.AreEqual(data.Count, 1);
                Assert.AreEqual(data.List.First().Contact.Forename, customer.Contact.Forename);
            }


            customerPost.Contact.Forename = "ChangedForename-" + DateTime.Now.Ticks;
            // PUT
            {
                var request = new RestRequest
                {
                    Token = Token,
                    Object = customerPost,
                    PageUrl = "Payment/Customers",
                    Host = "core-dev10.secupay-ag.de",
                    Id = customerPost.Id
                };

                var customerPut = RestService.PutObject<Customer>(request);

                // Assert.AreEqual(data, 1);
            }

            // GET
            {
                var request = new RestRequest
                {
                    Token = Token,
                    QueryParams = new QueryParams {Query = "id:" + customerPost.Id},
                    PageUrl = "Payment/Customers",
                    Host = "core-dev10.secupay-ag.de"
                };

                var customerGet = RestService.GetList<Customer>(request);
                Assert.AreEqual(customerGet.List.First().Contact.Forename, customerPost.Contact.Forename);
            }

            // DELETE
            {
                var request = new RestRequest
                {
                    Token = Token,
                    Id = customerPost.Id,
                    PageUrl = "Payment/Customers",
                    Host = "core-dev10.secupay-ag.de"
                };

                RestService.DeleteObject(request);

                // Assert.AreEqual(data, 1);
            }

            Thread.Sleep(1000);
            // GET
            {
                var request = new RestRequest
                {
                    Token = Token,
                    QueryParams = new QueryParams {Query = "id:" + customerPost.Id},
                    PageUrl = "Payment/Customers",
                    Host = "core-dev10.secupay-ag.de"
                };

                var customerGet = RestService.GetList<Customer>(request);
                Assert.AreEqual(0, customerGet.Count);
            }
        }
    }
}