﻿using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Api.Helpers
{
    public static class UrlExtensions
    {
        public static string ToPathSegment(this PathSegmentParameters parameters)
        {
            var uri = parameters.ApiVersion +
                      "/" + parameters.QuandlCode +
                      "." + parameters.ResponseFormat.ToLower();
            return uri;
        }

        public static Dictionary<string, string> ToQueryParameterDictionary(this RequestParameters options)
        {
            if (options == null) throw new NullReferenceException("options");
            return options.ToQueryParameters().ToDictionary(x => x.Name, x => x.Value);
        }

        public static IEnumerable<RequestParameter> ToQueryParameters(this RequestParameters options)
        {
            var parameters = new List<RequestParameter>();

            if (!String.IsNullOrEmpty(options.ApiKey))
            {
                var parameter = new RequestParameter(RequestParameterConstants.AuthToken, options.ApiKey);
                parameters.Add(parameter);
            }
            if (options.SortOrder.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.SortOrder,
                    options.SortOrder.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (options.ExcludeHeaders.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.ExcludeHeaders,
                    options.ExcludeHeaders.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (options.ExcludeData.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.ExcludeData,
                    options.ExcludeData.Value.GetStringValue());
                parameters.Add(parameter);
            }
            if (options.Rows.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Rows, options.Rows.Value.ToString());
                parameters.Add(parameter);
            }
            if (options.Frequency.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Frequency,
                    options.Frequency.Value.ToString());
                parameters.Add(parameter);
            }
            if (options.DateRange != null)
            {
                const string dateFormat = "yyyy-MM-dd";

                var parameter1 = new RequestParameter(RequestParameterConstants.TrimStart,
                    options.DateRange.TrimStart.ToString(dateFormat));
                parameters.Add(parameter1);

                var parameter2 = new RequestParameter(RequestParameterConstants.TrimEnd,
                    options.DateRange.TrimEnd.ToString(dateFormat));
                parameters.Add(parameter2);
            }
            if (options.Column.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Column, options.Column.Value.ToString());
                parameters.Add(parameter);
            }
            if (options.Transformation.HasValue)
            {
                var parameter = new RequestParameter(RequestParameterConstants.Transformation,
                    options.Transformation.Value.GetStringValue());
                parameters.Add(parameter);
            }
            return parameters;
        }

      

        public static string ToUrl(this QuandlRestClientRequestParameters parameters, string baseUrl)
        {
            if (string.IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");
            
            var url = baseUrl.AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }

        public static string ToUri(this QuandlRestClientRequestParameters parameters)
        {
            if (string.IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");

            var url = "api".AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }
    }
}