using Autofac;
using Customers.Events;
using Customers.Exceptions;
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

            MapBsonClassMaps();
        }

        private void MapBsonClassMaps()
        {
            BsonClassMap.RegisterClassMap<Name>(cm =>
            {
                cm.MapProperty(x => x.First);
                cm.MapProperty(x => x.Last);
                cm.MapProperty(x => x.Middle);
                cm.MapCreator(x => new Name(x.First, x.Middle, x.Last));
            });


            BsonClassMap.RegisterClassMap<EmailAddress>(cm =>
            {
                cm.MapProperty(x => x.Value);
                cm.MapCreator(x => x.Value);
            });


            BsonClassMap.RegisterClassMap<Customer>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id);
            });

            BsonClassMap.RegisterClassMap<DomainEvent<Customer>>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.AggregateType).SetElementName("AggregateType");
            });

            BsonClassMap.RegisterClassMap<NewCustomerCreated>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.Customer).SetElementName("Customer");
            });
        }
    }
}
