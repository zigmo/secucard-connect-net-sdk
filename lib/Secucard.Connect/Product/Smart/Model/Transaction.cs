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

namespace Secucard.Connect.Product.Smart.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;

    [DataContract]
    public class Transaction : SecuObject
    {
        public Transaction()
        {
            TypedStatus = StatusEnum.Unknown;
        }

        public enum StatusEnum
        {
            Unknown,
            Aborted,
            Canceled,
            Created,
            Failed,
            Finished,
            OK,
            Timeout
        }

        private static readonly IDictionary<string, StatusEnum> StatusStrings = new Dictionary<string, StatusEnum>
        {
            {"aborted", StatusEnum.Aborted},
            {"canceled", StatusEnum.Canceled},
            {"created", StatusEnum.Created},
            {"failed", StatusEnum.Failed},
            {"finished", StatusEnum.Finished},
            {"ok", StatusEnum.OK},
            {"timeout", StatusEnum.Timeout}
        };

        [DataMember(Name = "device_source", EmitDefaultValue = false)]
        public Device DeviceSource { get; set; }

        [DataMember(Name = "basket_info")]
        public BasketInfo BasketInfo { get; set; }

        [DataMember(Name = "target_device", EmitDefaultValue = false)]
        public Device TargetDevice { get; set; }

        public event EventHandler<StatusEnum> StatusChanged;
        public StatusEnum TypedStatus { get; private set; }

        private string _status;

        [DataMember(Name = "status")]
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status == value) return;
                _status = value;
                if (StatusStrings.ContainsKey(Status))
                {
                    TypedStatus = StatusStrings[Status];
                }
                else
                {
                    TypedStatus = StatusEnum.Unknown;
                }

                StatusChanged?.Invoke(this, TypedStatus);
            }
        }

        [DataMember(Name = "created")]
        public string FormattedCreated
        {
            get { return Created.ToDateTimeZone(); }
            set { Created = value.ToDateTime(); }
        }

        public DateTime? Created { get; set; }

        [DataMember(Name = "updated")]
        public string FormattedUpdated
        {
            get { return Updated.ToDateTimeZone(); }
            set { Updated = value.ToDateTime(); }
        }

        public DateTime? Updated { get; set; }

        [DataMember(Name = "idents")]
        public List<Ident> Idents { get; set; }

        [DataMember(Name = "basket")]
        public Basket Basket { get; set; }

        [DataMember(Name = "merchantRef")]
        public string MerchantRef { get; set; }

        [DataMember(Name = "transactionRef")]
        public string TransactionRef { get; set; }

        [DataMember(Name = "payment_method", EmitDefaultValue = false)]
        public string PaymentMethod { get; set; }

        [DataMember(Name = "receipt", EmitDefaultValue = false)]
        public List<ReceiptLine> ReceiptLines { get; set; }

        [DataMember(Name = "payment_requested", EmitDefaultValue = false)]
        public string PaymentRequested { get; set; }

        [DataMember(Name = "payment_executed", EmitDefaultValue = false)]
        public string PaymentExecuted { get; set; }

        [DataMember(Name = "error", EmitDefaultValue = false)]
        public string Error { get; set; }

        [DataMember(Name = "texts", EmitDefaultValue = false)]
        public List<string> Texts { get; set; }

        public override string ToString()
        {
            return "Transaction{" +
                   "basketInfo=" + BasketInfo +
                   ", deviceSource=" + DeviceSource +
                   ", targetDevice=" + TargetDevice +
                   ", status='" + Status + '\'' +
                   ", created=" + Created +
                   ", idents=" + Idents +
                   ", basket=" + Basket +
                   ", merchantRef='" + MerchantRef + '\'' +
                   ", transactionRef='" + TransactionRef + '\'' +
                   "} " + base.ToString();
        }
    }
}