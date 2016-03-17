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

namespace Secucard.Connect.Product.General
{
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Product.General.Model;

    public class SkeletonsServiceStomp : ProductService<Skeleton>
    {
        public static readonly ServiceMetaData<Skeleton> MetaData = new ServiceMetaData<Skeleton>(
            "general", "skeletons");

        protected override ServiceMetaData<Skeleton> GetMetaData()
        {
            return MetaData;
        }

        protected override ChannelOptions GetDefaultOptions()
        {
            var channelOptions = base.GetDefaultOptions();
            channelOptions.Channel = ChannelOptions.ChannelStomp;
            return channelOptions;
        }
    }
}