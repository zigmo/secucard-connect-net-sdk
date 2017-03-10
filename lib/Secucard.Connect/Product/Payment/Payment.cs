/*
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

namespace Secucard.Connect.Product.Payment
{
    /// <summary>
    /// Holds service references and service type constants for "payment" product.
    /// </summary>
    public class Payment
    {
        public ContainersService Containers { get; set; }

        public ContractService Contracts { get; set; }

        public CustomerPaymentService Customers { get; set; }

        public SecupayDebitsService Secupaydebits { get; set; }

        public SecupayPrepaysService Secupayprepays { get; set; }

        public SecupayCreditcardsService Secupaycreditcards { get; set; }

        public SecupayInvoicesService Secupayinvoices { get; set; }
    }
}