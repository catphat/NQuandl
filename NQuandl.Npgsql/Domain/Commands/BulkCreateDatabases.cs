﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reactive.Linq;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using Npgsql;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Api.Transactions;
//using NQuandl.Npgsql.Domain.Entities;
//using NQuandl.Npgsql.Services.Extensions;

//namespace NQuandl.Npgsql.Domain.Commands
//{
//    public class BulkCreateDatabases : IDefineCommand
//    {
//        public IObservable<Database> Databases { get; private set; }

//        public BulkCreateDatabases([NotNull] IObservable<Database> databases)
//        {
//            if (databases == null)
//                throw new ArgumentNullException(nameof(databases));
//            Databases = databases;
//        }
//    }

//    public class HandleBulkCreateDatabases : IHandleCommand<BulkCreateDatabases>
//    {
//        private readonly IConfigureConnection _configuration;
//        private readonly IMapDataRecordToEntity<Database> _mapper;


//        public HandleBulkCreateDatabases([NotNull] IConfigureConnection configuration,
//            [NotNull] IMapDataRecordToEntity<Database> mapper)
//        {
//            if (configuration == null)
//                throw new ArgumentNullException(nameof(configuration));
//            if (mapper == null)
//                throw new ArgumentNullException(nameof(mapper));
//            _configuration = configuration;
//            _mapper = mapper;
//        }

//        public Task Handle(BulkCreateDatabases command)
//        {
           

             
//            },
//                onCompleted: () => DisposeConnectionAndWrite(connection, writer),
//                onError:
//                    exception => { throw new Exception(exception.Message); });

//            return Task.FromResult(0);
//        }

//        private static void DisposeConnectionAndWrite(NpgsqlConnection connection, NpgsqlBinaryImporter importer)
//        {
//            importer.Close();
//            importer.Dispose();
//            connection.Dispose();
//        }
//    }
//}
