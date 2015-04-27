﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models.QuandlRequests
{
    public class RequestString : BaseQuandlRequestV1<RequestStringResponse>
    {
        private readonly string _queryCode;
        public RequestString(string querycode)
        {
            _queryCode = querycode;
        }

        public override string QueryCode
        {
            get { return _queryCode; }
        }

        public OptionalRequestParameters OptionalRequestParameter { get; set; }
    }

    public class RequestStringResponse : QuandlResponse
    {
        public string String { get; set; }
    }
}
