using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MongoDB.Driver;
using EventStore;

namespace Account.Autofac
{
    public class RepositoryModule : Module
    {
        private readonly string mongoDbConnection;

        public RepositoryModule(string mongoDbConnection)
        {
            this.mongoDbConnection = mongoDbConnection;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (string.IsNullOrEmpty(mongoDbConnection)) throw new Exception("Unable to find mongodb connection string");
        
            builder.Register(c => new MongoClient(mongoDbConnection)).As<IMongoClient>().SingleInstance(); //thread safe connection

            builder.RegisterType<MongoEventStore.MongoEventStore>().As<IEventStore>();

            builder.RegisterType<BankAccountRepository>().As<IBankAccountRepository>();

            BsonClassMappings.Configure();

        }
    }
}

