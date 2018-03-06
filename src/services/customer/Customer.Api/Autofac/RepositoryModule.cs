using Autofac;
using Customers.Events;
using Customers.Exceptions;
using Domain;
using Domain.DomainEvents;
using EventStore;
using MongoDB.Driver;

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
                   
            builder.RegisterType<MongoEventStore.MongoEventStore>().As<IEventStore>();

            BsonClassMappings.Configure();
        }      
    }
}
