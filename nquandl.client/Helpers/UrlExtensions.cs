﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Flurl;
using NQuandl.Client.Requests;

namespace NQuandl.Client.Helpers
{
    public static class UrlExtensions
    {
        public static string ToUriV1(this RequiredRequestParameters parameters)
        {
            var uri = parameters.ApiVersion +
                      "/" + parameters.QuandlCode +
                      "." + parameters.ResponseFormat;
            return uri;
        }

        public static List<QueryParameter> ToQueryParameters(this RequestParameterOptions options)
        {
            var parameters = new List<QueryParameter>();

            if (!String.IsNullOrEmpty(options.ApiKey))
            {
                var parameter = new QueryParameter(RequestParameterConstants.AuthToken, options.ApiKey);
                parameters.Add(parameter);
            }
            if (options.SortOrder.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.SortOrder,
                    options.SortOrder.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (options.ExcludeHeaders.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.ExcludeHeaders,
                    options.ExcludeHeaders.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (options.Rows.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.Rows, options.Rows.Value.ToString());
                parameters.Add(parameter);
            }
            if (options.DateRange != null)
            {
                const string dateFormat = "yyyy-MM-dd";

                var parameter1 = new QueryParameter(RequestParameterConstants.TrimStart,
                    options.DateRange.TrimStart.ToString(dateFormat));
                parameters.Add(parameter1);

                var parameter2 = new QueryParameter(RequestParameterConstants.TrimEnd,
                    options.DateRange.TrimEnd.ToString(dateFormat));
                parameters.Add(parameter2);
            }
            if (options.Column.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.Column, options.Column.Value.ToString());
                parameters.Add(parameter);
            }
            if (options.Transformation.HasValue)
            {
                var parameter = new QueryParameter(RequestParameterConstants.Transformation,
                    options.Transformation.Value.GetStringValue());
                parameters.Add(parameter);
            }
            return parameters;
        }

        public static string ToQueryUri(this RequestParameterOptions options)
        {
            var uri = string.Empty;
            if (options == null) return uri;
            return uri.SetQueryParams(options);
        }

        public static string ToQueryUri(this List<QueryParameter> parameters)
        {
            if (parameters.Count == 0)
            {
                return string.Empty;
            }

            var uri = new StringBuilder();

            var stringList =
                parameters.Select(serviceParameter => serviceParameter.Name + "=" + serviceParameter.Value).ToList();
            uri.Append("?");
            uri.Append(stringList.First());
            foreach (var parameter in parameters.Skip(1))
            {
                uri.Append("&" + parameter.Name + "=" + parameter.Value);
            }

            return uri.ToString();
        }
    }
}