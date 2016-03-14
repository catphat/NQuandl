﻿using NQuandl.Api;
using SimpleInjector;

namespace NQuandl.Services.Quandl
{
    public static class CompositionRoot
    {
        public static void RegisterQuandlClient(this Container container)
        {
            container.Register<IQuandlClient, QuandlClient>();
        }
    }
}