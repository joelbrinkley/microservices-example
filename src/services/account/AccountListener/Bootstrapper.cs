using Account.Autofac;
using Account.Listener;
using AccountListener.EventProcessors;
using Autofac;
using EventListener;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountListener
{
    public static class Bootstrapper
    {
        public static IContainer Run()
        {
            var builder = new ContainerBuilder();

            var mongoConnection = Config.MONGO_CONNECTION;
            var elkUrl = Config.ELK_URL;
            var customerUrl = Config.CUSTOMER_URL;

            if (string.IsNullOrEmpty(mongoConnection))
            {
                throw new Exception("The mongodb connection environment variable is missing.  Please check env variable MONGODB");
            }

            if (string.IsNullOrEmpty(elkUrl))
            {
                throw new Exception("The elk environment variable is missing.  Please check env variable ELK");
            }

            if (string.IsNullOrEmpty(customerUrl))
            {
                throw new Exception("The customer url environment variable is missing.  Please check env variable CUSTOMER_URL");
            }

            Log.Logger = new LoggerConfiguration()
              .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elkUrl))
              {
                  IndexFormat = "microexample-account-listener-{0:yyyy.MM}",
                  MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information
              })
              .WriteTo.Console()
              .CreateLogger();

            builder.RegisterModule(new ServiceProxyModule(customerUrl));
            builder.RegisterModule(new RepositoryModule(mongoConnection));
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new LoggingModule());

            builder.RegisterType<NewCustomerEventProcessor>().AsSelf().AsImplementedInterfaces();

            //build even maps
            builder.Register<EventProcessorMap>(c =>
            {
                var eventMaps = new EventProcessorMap();

                eventMaps.Add("Customer.Created", c.Resolve<NewCustomerEventProcessor>());

                return eventMaps;
            });

            builder.RegisterType<EventProcessor>().As<IEventProcessor>()
              .WithParameter("brokerUrl", Config.BROKER_URL);

            return builder.Build();
        }
    }
}
