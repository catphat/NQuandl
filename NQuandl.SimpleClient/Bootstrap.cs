﻿

using Microsoft.Framework.Configuration;
using NQuandl.Services.CompositionRoot;
using NQuandl.Services.Npgsql.CompositionRoot;
using SimpleInjector;

namespace NQuandl.SimpleClient
{
    public static class Bootstrapper
    {
        public static Container Bootstrap()
        {
            var container = new Container();
            var builder = new ConfigurationBuilder();
            //builder.AddJsonFile(@"App_Data\config.json");

            //var rootCompositionSettings = new RootCompositionSettings
            //{
            //    Configuration = builder.Build().GetSection("AppSettings")
            //};
            //container.ComposeRoot(rootCompositionSettings);

            container.ComposeRoot();
            container.RegisterNpsqlServices();
            container.Verify();

            return container;
        }
    }
}