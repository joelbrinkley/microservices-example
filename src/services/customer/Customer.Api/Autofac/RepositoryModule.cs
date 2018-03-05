using Autofac;
using Customers.Events;
using Customers.Exceptions;
using Domain;
using Domain.DomainEvents;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers
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
            if (string.IsNullOrEmpty(mongoDbConnection)) throw new ApplicationConfigurationException("Unable to find mongodb connection string");

            builder.Register(c => new MongoClient(mongoDbConnection)).As<IMongoClient>().SingleInstance(); //thread safe connection

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();

            builder.RegisterType<EventStream>().As<IEventStream<Customer>>();

            BsonClassMappings.Configure();
        }      
    }
}
