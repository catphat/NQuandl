﻿using System.Linq;
using System.Reactive.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using NQuandl.Npgsql.Domain.Commands;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Database;
using NQuandl.Npgsql.Services.Database.Customization;
using NQuandl.Npgsql.Services.Database.Initialization;
using NQuandl.Npgsql.Services.Metadata;
using NQuandl.Npgsql.Tests.Integration._Fixtures;
using Xunit;

namespace NQuandl.Npgsql.Tests.Integration
{
    public class DatabaseIntegrationTests : DatabaseTests
    {
        public DatabaseIntegrationTests(DatabaseFixture fixture) : base(fixture) {}

       

        [Fact]
        public void DatabaseConnectionIsValid()
        {
            var connection = Connection.CreateConnection();
            connection.Open();
        }

        [Fact]
        public void DatabaseGreenfieldIntializationIsValid()
        {
            var initializer = new GreenfieldDbInitializer(new PostgresSqlScriptsCustomizer());
            initializer.Intialize(DbContext);
        }

        [Fact]
        public async void InsertCountryDataCommand()
        {
            var command = new WriteEntity<Country>(TestCountry);
            var commandHandler = new HandleWriteEntity<Country>(DbContext, new EntityMetadataCache<Country>(new EntityMetadataCacheInitializer<Country>()));
            await commandHandler.Handle(command);
        }

        [Fact]
        public async void EntitiesObservableByQuery()
        {
            var query = new EntitiesObservableBy<Country>();
            var queryHandler = new HandleEntitiesObservableBy<Country>(new EntityMetadataCache<Country>(new EntityMetadataCacheInitializer<Country>()), DbContext);
            var result = queryHandler.Handle(query);
            var results = await result.ToList();
            var firstResult = results.FirstOrDefault();
            Assert.NotNull(firstResult);
            firstResult.ShouldBeEquivalentTo(TestCountry);
           
        }
    }
}