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

namespace Secucard.Connect.Product.Document
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Product.Document.Model;

    public class UploadsService : ProductService<Upload>
    {
        public static readonly ServiceMetaData<Upload> MetaData = new ServiceMetaData<Upload>("document", "uploads");

        protected override ServiceMetaData<Upload> GetMetaData()
        {
            return MetaData;
        }

        /// <summary>
        ///  Upload the given document and returns the new id for the upload.
        ///  Note: the uploaded content should be base64 encoded.
        /// </summary>
        public string Upload(Upload content)
        {
            Upload result = Execute<Upload>(null, null, null, content, null);
            return result.Id;
        }
    }
}