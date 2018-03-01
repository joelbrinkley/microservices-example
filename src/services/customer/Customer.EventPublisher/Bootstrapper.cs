using Autofac;
using Customers.Logging;
using MongoDB.Driver;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.EventPublisher
{
    public static class Bootstrapper
    {
        public static IContainer Run()
        {
            var builder = new ContainerBuilder();

            var mongoConnectionString = Config.MONGODB;
            var elkUrl = Config.ELK_URL;

            if (string.IsNullOrEmpty(mongoConnectionString))
            {
                throw new MongoConfigurationException("The mongodb environment variable is missing.  Please check env variable MONGODB");
            }

            if (string.IsNullOrEmpty(elkUrl))
            {
                throw new MongoConfigurationException("The elk environment variable is missing.  Please check env variable ELK");
            }

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elkUrl))
            {
                IndexFormat = "microexample-customer-eventpublisher-{0:yyyy.MM}",
                MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information
            })
            .WriteTo.Console()
            .CreateLogger();

            builder.Register(c => new MongoClient(mongoConnectionString)).As<IMongoClient>().SingleInstance(); //thread safe connection
            builder.RegisterType<PublisherService<Customer>>().AsSelf();
            builder.RegisterType<SerilogAdapter>().As<ILog>();

            BsonClassMappings.Configure();

            return builder.Build();
        }
    }
}
