﻿using System.Runtime.Serialization;

namespace Zigmo.Secucard.Connect.NetCore.Auth.Model
{
    [DataContract]
    public class AuthCodeError
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "error_description")]
        public string ErrorDescription { get; set; }

        public override string ToString()
        {
            return string.Format("DeviceAuthCode{{" + "error='{0}', ErrorDescription='{1}'}}", Error, ErrorDescription);
        }
    }
}