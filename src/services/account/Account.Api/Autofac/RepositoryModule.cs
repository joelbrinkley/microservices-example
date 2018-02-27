using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Account.Exceptions;
using Account.Customers;
using MongoDB.Driver;
using MongoEventStore;
using EventStore;

namespace Account.Autofac
{
    public class RepositoryModule : Module
    {
        private readonly string mongoDbConnection;
        private readonly string customerBaseUrl;

        public RepositoryModule(string mongoDbConnection, string customerBaseUrl)
        {
            this.mongoDbConnection = mongoDbConnection;
            this.customerBaseUrl = customerBaseUrl;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (string.IsNullOrEmpty(mongoDbConnection)) throw new ApplicationConfigurationException("Unable to find mongodb connection string");
            if (string.IsNullOrEmpty(customerBaseUrl)) throw new ApplicationConfigurationException("Unable to find customer service base url");

            builder.Register(c => new MongoClient(mongoDbConnection)).As<IMongoClient>().SingleInstance(); //thread safe connection

            builder.RegisterType<MongoEventStore.MongoEventStore>().As<IEventStore>();

            builder.RegisterType<BankAccountRepository>().As<IBankAccountRepository>();

            builder.RegisterType<CustomerRepositoryProxy>().As<ICustomerRepository>().WithParameter("baseUrl", customerBaseUrl);
            
        }
    }
}

