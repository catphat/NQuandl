﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SimpleInjector;

namespace NQuandl.Npgsql.Tests.Unit.SimpleInjector._Fixtures
{
    public class CompositionRootFixture
    {

        public Container Container { get; private set; }

        public CompositionRootFixture()
        {
           
            Container = new Container();
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            var settings = new RootCompositionSettings
            {
                FluentValidatorAssemblies = assemblies,
                QueryHandlerAssemblies = assemblies,
                CommandHandlerAssemblies = assemblies,
            };
            Container.ComposeRoot(settings);
        }
    }
}