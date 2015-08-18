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

namespace Secucard.Connect.Product.Service.Model.services.idresult
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Address
    {
        [DataMember(Name = "postal_code")]
        public ValueClass postalCode { get; set; }

        [DataMember(Name = "country")]
        public ValueClass country { get; set; }

        [DataMember(Name = "city")]
        public ValueClass city { get; set; }

        [DataMember(Name = "street")]
        public ValueClass street { get; set; }

        [DataMember(Name = "street_number")]
        public ValueClass streetNumber { get; set; }

        public override string ToString()
        {
            return "Address{" +
                   "zipcode=" + postalCode +
                   ", country=" + country +
                   ", city=" + city +
                   ", street=" + street +
                   ", streetNumber=" + streetNumber +
                   '}';
        }
    }
}