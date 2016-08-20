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

namespace Secucard.Connect.Auth.Exception
{
    using Secucard.Connect.Client;

    /// <summary>
    /// Indicates an authentication was aborted due a timeout.
    /// </summary>
    public class AuthTimeoutException : AuthError
    {
        private string _error;

        public string GetError()
        {
            return _error;
        }

        public void SetError(string error)
        {
            _error = error;
        }

        public AuthTimeoutException(string message, string error)
            : base(message)
        {
            _error = error;
        }

        public AuthTimeoutException(string message)
            : base(message)
        {
        }

        public AuthTimeoutException(string message, System.Exception cause)
            : base(message, cause)
        {
        }
    }
}