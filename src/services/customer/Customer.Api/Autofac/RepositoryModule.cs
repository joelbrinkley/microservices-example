using Autofac;
using Customers.Exceptions;
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

            builder.Register(c => new MongoClient(mongoDbConnection)).SingleInstance(); //thread safe connection

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();

        }
    }
}
