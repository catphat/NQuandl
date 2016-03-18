﻿using NQuandl.Api.Quandl;
using SimpleInjector;

namespace NQuandl.Services.HttpClient
{
    public static class CompositionRoot
    {
        public static void RegisterHttpClient(this Container container)
        {
            container.RegisterSingleton<IHttpClient, HttpClient>();
            container.RegisterDecorator<IHttpClient, HttpClientDebugDecorator>(Lifestyle.Transient);
            container.RegisterDecorator<IHttpClient, HttpClientRateLimiterDecorator>(Lifestyle.Transient);
            container.RegisterDecorator<IHttpClient, HttpClientTaskQueueDecorator>(Lifestyle.Transient);
            container.RegisterDecorator<IHttpClient, HttpClientLoggerDecorator>(Lifestyle.Transient);
        }
    }
}