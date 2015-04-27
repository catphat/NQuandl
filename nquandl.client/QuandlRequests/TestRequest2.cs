﻿using System;

namespace NQuandl.Client
{
    public class TestRequest2 : BaseQuandlRequest<TestResponse2>
    {
        public override string QueryCode
        {
            get { throw new NotImplementedException(); }
        }

        public override string Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public override string Url
        {
            get { return "http://localhost:49832/api/testapi2/"; }
        }
    }

  
    public class TestResponse2 : QuandlResponse
    {
        public string RequestType { get; set; }
        public string UniqueId2 { get; set; }
        public int RequestCount { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Milliseconds { get; set; }
    }





}


