using Autofac;
using Logging;
using MongoDB.Driver;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace MongoEventStorePublisher
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
                throw new Exception("The mongodb environment variable is missing.  Please check env variable MONGODB");
            }

            if (string.IsNullOrEmpty(elkUrl))
            {
                throw new Exception("The elk environment variable is missing.  Please check env variable ELK");
            }

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elkUrl))
            {
                IndexFormat = "microexample-mongo-event-store-{0:yyyy.MM}",
                MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information
            })
            .WriteTo.Console()
            .CreateLogger();

            builder.Register(c => new MongoClient(mongoConnectionString)).As<IMongoClient>().SingleInstance(); //thread safe connection
            builder.RegisterType<PublisherService>().AsSelf().WithParameter("natsConnection", Config.BROKER_URL);
            builder.RegisterType<SerilogAdapter>().As<ILog>();
            
            return builder.Build();
        }
    }
}
