﻿using NQuandl.Client.Api;
using NQuandl.Client.Domain;
using NQuandl.Client.Domain.Queries;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NQuandl.Client.CompositionRoot
{
    public static class ContainerRegistrations
    {
        public static void RegisterHttpClient(this Container container, string baseUrl)
        {
            container.RegisterSingle<IHttpClient>(() => new HttpClient(baseUrl));
        }

        public static void RegisterQuandlRestClient(this Container container)
        {
#if DEBUG
    //http://localhost:49832/api
            container.Register<IQuandlRestClient>(() => new QuandlRestClient(baseUrl));
#else
            //https://quandl.com/api
            container.Register<IQuandlRestClient>(() => new QuandlRestClient(container.GetInstance<IHttpClient>()));
#endif
        }

        public static void RegisterQuandlClient(this Container container)
        {
            container.Register<IQuandlClient>(() => new QuandlClient(container.GetInstance<IQuandlRestClient>()));
        }

        public static void RegisterQuandlJsonClient(this Container container)
        {
            container.Register<IQuandlJsonClient>(
                () =>
                    new QuandlJsonClient(container.GetInstance<IQuandlClient>(),
                        container.GetInstance<IProcessQueries>()));
        }

        public static void RegisterMapper(this Container container)
        {
            container.RegisterManyForOpenGeneric(typeof (IMapObjectToEntity<>), typeof (IMapObjectToEntity<>).Assembly);
        }

        //todo: openGeneric registrations can probably be consolidated to a single simple injector api call
        public static void RegisterQueries(this Container container)
        {
            container.RegisterSingle<IProcessQueries, QueryProcessor>();

            var assembly = typeof (IHandleQuery<,>).Assembly;

            container.RegisterManyForOpenGeneric(typeof (IHandleQuery<,>), assembly);
            container.RegisterOpenGeneric(typeof (IHandleQuery<,>), typeof (HandleGetQuandlCodeByEntity<>));
            container.RegisterOpenGeneric(typeof (IHandleQuery<,>), typeof (HandleDeserializeToClass<>));
            container.RegisterOpenGeneric(typeof (IHandleQuery<,>), typeof (HandleDeserializeToJsonResponseV1<>));
            container.RegisterOpenGeneric(typeof (IHandleQuery<,>), typeof (HandleMapToEntitiesByDataObjects<>));
            container.RegisterOpenGeneric(typeof (IHandleQuery<,>), typeof (HandleRequestJsonResponseV1ByEntity<>));
        }
    }
}