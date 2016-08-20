/*
 * Copyright (c) 2016. hp.weber GmbH & Co secucard KG (www.secucard.com)
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Runtime.CompilerServices;
using System.Threading;
using Secucard.Connect.Product.Payment.Model;

namespace Secucard.Connect.DemoApp
{
    using System;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Auth.Model;
    using Secucard.Connect.Client.Config;
    using Secucard.Connect.Storage;

    internal class Program
    {
        private static ClientConfiguration _clientConfiguration;

        private static void Main(string[] args)
        {
            // Load default properties
            var properties = Properties.Load("SecucardConnect.config");

            // Perpare client config. Implement your own Auth Details
            _clientConfiguration = new ClientConfiguration(properties)
            {
                ClientAuthDetails = new ClientAuthDetailsDeviceToBeImplemented(),
                DataStorage = new MemoryDataStorage()
            };

            // Create client and attach client event handlers
            var client = SecucardConnect.Create(_clientConfiguration);
            client.Open();

            var customer = new Customer
            {
                Contact = new Product.General.Model.Contact
                {
                    Salutation = "Mr.",
                    Title = "Dr.",
                    Forename = "John",
                    Surname = "Doe",
                    CompanyName = "Example Inc.",
                    DateOfBirth = new DateTime(1901, 2, 3),
                    Email = "example@example.com",
                    Phone = "0049-123-456789",
                    Mobile = "0049-987-654321",
                    Address = new Connect.Product.General.Model.Address
                    {
                        City = "Examplecity",
                        Country = "Germany",
                        PostalCode = "01234",
                        Street = "Example Street",
                        StreetNumber = "6a"
                    }
                }
            };

            customer = client.Payment.Customers.Create(customer);
            Console.WriteLine("Customer ID: " + customer.Id);

            var container = new Container
            {
                Type = "bank_account",
                PrivateData = new Data
                {
                    Owner = "John Doe",
                    Iban = "DE89370400440532013000",
                    Bic = "37040044"
                }
            };

            container = client.Payment.Containers.Create(container);
            Console.WriteLine("Container ID: " + container.Id);


            var debit = new SecupayDebit
            {
                Customer = customer,
                Container = container,
                Amount = 100,
                Currency = "EUR",
                Purpose = "purpose",
                OrderId = "orderid"
            };

            debit = client.Payment.Secupaydebits.Create(debit);
            Console.WriteLine("Debit ID: " + debit.Id);

            Console.WriteLine("The following Debit has been added:" + Environment.NewLine);
            Console.WriteLine(debit + Environment.NewLine);
            Console.WriteLine("Until clearance, which happens once a day, the debit can be deleted. This functionality does not work with a demo contract however. After clearance, a web hook on the client's side can be invoked to inform you about the status change, and the money is automatically transfered to your account.");

            Console.WriteLine("When your web hook is invoked, you receive the Id of the updated debit. You can get additional details as follows:");
            debit = client.Payment.Secupaydebits.Get(debit.Id);

            Console.WriteLine("Deleting the debit can be done as follows:");
            client.Payment.Secupaydebits.Delete<SecupayDebit>(debit.Id);

            var prepay = new SecupayPrepay
            {
                Customer = customer,
                Amount = 100,
                Currency = "EUR",
                Purpose = "purpose",
                OrderId = "orderid"
            };

            prepay = client.Payment.Secupayprepays.Create(prepay);
            Console.WriteLine("Prepay ID: " + prepay.Id);

            Console.WriteLine("The following Prepay has been added:" + Environment.NewLine);
            Console.WriteLine(prepay + Environment.NewLine);
            Console.WriteLine("Until the customer has paid, the prepay can be deleted. After payment, a web hook on the client's side can be invoked to inform you about the status change, and the money is automatically transfered to your account.");

            Console.WriteLine("The web hook will receive data which is structured as follows:");

            Console.WriteLine("When your web hook is invoked, you receive the Id of the updated prepay. You can get additional details as follows:");
            prepay = client.Payment.Secupayprepays.Get(prepay.Id);

            Console.WriteLine("Deleting the debit can be done as follows:");
            client.Payment.Secupayprepays.Delete<SecupayPrepay>(prepay.Id);
        }

        /// <summary>
        /// Implement your own 
        /// </summary>
        private class ClientAuthDetailsDeviceToBeImplemented : AbstractClientAuthDetails, IClientAuthDetails
        {
            public OAuthCredentials GetCredentials()
            {
                return new ClientCredentials(
                    "e6f43edf21ce7b50194207716b88ad87",
                    "d21c65c46f50798e6772da6095b6708cf17c80bd141d037ae819a3cbcd8185f1");
            }

            public ClientCredentials GetClientCredentials()
            {
                return (ClientCredentials)GetCredentials();
            }
        }
    }
}