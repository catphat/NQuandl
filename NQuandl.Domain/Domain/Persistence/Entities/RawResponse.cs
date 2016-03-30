﻿using NQuandl.Api.Persistence;
using NQuandl.Api.Persistence.Entities;

namespace NQuandl.Domain.Persistence.Entities
{
    public class RawResponse : EntityWithId<int>
    {
        public string RequestUri { get; set; }
        public string ResponseContent { get; set; }
    }
}