﻿using System;
using Newtonsoft.Json;
using NQuandl.Client.Helpers;

namespace NQuandl.Client.Requests
{
    public class QueryParametersV1
    {
        public string ApiKey { get; set; }
        public SortOrder? SortOrder { get; set; }
        public Exclude? ExcludeHeaders { get; set; }
        public int? Rows { get; set; }
        public DateRange DateRange { get; set; }
        public int? Column { get; set; }
        public Transformation? Transformation { get; set; }
        public Exclude? ExcludeData { get; set; }
        public Frequency? Frequency { get; set; }
    }

    public class QueryParametersV2
    {
        public string ApiKey { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Query { get; set; }
        public string SourceCode { get; set; }
    }

    public class DateRange
    {
        public DateTime TrimStart { get; set; }
        public DateTime TrimEnd { get; set; }
    }
}