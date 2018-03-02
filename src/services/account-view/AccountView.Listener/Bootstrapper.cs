
using AccountView.Data;
using AccountView.Listener.ContextFactory;
using AccountView.Listener.EventProcessors;
using Autofac;
using Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Listener
{
    public static class Bootstrapper
    {
        public static IContainer Run()
        {
            var builder = new ContainerBuilder();

            var sqlConnection = Config.SqlConnection;
            var elkUrl = Config.ELK_URL;

            if (string.IsNullOrEmpty(sqlConnection))
            {
                throw new Exception("The sql connection environment variable is missing.  Please check env variable SQL_CONNECTION");
            }

            if (string.IsNullOrEmpty(elkUrl))
            {
                throw new Exception("The elk environment variable is missing.  Please check env variable ELK");
            }
            
            Log.Logger = new LoggerConfiguration()
           .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elkUrl))
           {
               IndexFormat = "microexample-accountview-listener-{0:yyyy.MM}",
               MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information
           })
           .WriteTo.Console()
           .CreateLogger();

            var optionsBuilder = new DbContextOptionsBuilder<AccountViewContext>();
            optionsBuilder.UseSqlServer(sqlConnection);

            builder.RegisterType<SerilogAdapter>().As<ILog>();

            builder.RegisterType<NewCustomerEventProcessor>().AsSelf().AsImplementedInterfaces();

            builder.RegisterType<AccountViewDbContextFactory>().As<IAccountViewContextFactory>().WithParameter("options", optionsBuilder.Options);

            //build even maps
            builder.Register<IDictionary<string, IProcessEvents>>(c =>
            {
                var eventMaps = new Dictionary<string, IProcessEvents>();

                eventMaps.Add("Customer.Created", c.Resolve<NewCustomerEventProcessor>());

                return eventMaps;
            });

            builder.RegisterType<EventProcessor>().As<IEventProcessor>();

            return builder.Build();
        }
    }
}
