﻿
using NQuandl.Client.Helpers;

namespace NQuandl.Client
{
    public abstract class QuandlResponse
    {

    }

    public interface IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        string Url { get; }
    }

    public abstract class BaseQuandlRequestV1<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        protected BaseQuandlRequestV1(QuandlCode quandlCode)
        {
            _quandlCode = quandlCode;
        }

        public OptionalRequestParameters Options { get; set; }

        private readonly QuandlCode _quandlCode;
        public string Url
        {
            get
            {
                var url = QuandlServiceConfiguration.BaseUrl + "/" + RequestParameterConstants.Version1Format + "/" +
                          _quandlCode.DatabaseCode + _quandlCode.TableCode + RequestParameterConstants.JsonFormat + "?" +
                          RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey);

                if (Options == null)
                {
                    return url;
                }
                return url + Options.ToRequestParameter();
            }
        }
    }




    public abstract class BaseQuandlRequestV2<TResponse> : IQuandlRequest<TResponse> where TResponse : QuandlResponse
    {
        public string QueryCode { get; set; }

        public string Url
        {
            get { return QuandlServiceConfiguration.BaseUrl + RequestParameterConstants.Version2Format + "." + RequestParameterConstants.JsonFormat + "?query=*&source_code=" + QueryCode + "&" + RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey); }
        }
    }

}
