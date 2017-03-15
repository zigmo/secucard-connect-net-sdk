﻿namespace Secucard.Connect.Product.Payment.Model
{
    using Common.Model;
    using System.Runtime.Serialization;

    [DataContract]
    public class RedirectUrl : SecuObject
    {
        /// <summary>
        /// The url for redirect the customer back to the shop after a successful payment checkout
        /// </summary>
        [DataMember(Name = "url_success")]
        public string UrlSuccess { get; set; }

        /// <summary>
        /// The url for redirect the customer back to the shop after a failure (or on cancel) on the payment checkout page
        /// </summary>
        [DataMember(Name = "url_failure")]
        public string UrlFailure;

        /// <summary>
        /// The url for redirect the customer to the payment checkout page
        /// </summary>
        [DataMember(Name = "iframe_url")]
        public string UrlIframe;
    }
}
