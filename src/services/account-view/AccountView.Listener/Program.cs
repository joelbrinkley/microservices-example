using System;
using System.Threading;
using Autofac;
using Logging;
using NATS.Client;
using AccountView.Listener.EventProcessors;
using EventListener;
using AccountView.Data;
using Microsoft.EntityFrameworkCore;
using AccountView.Listener.ContextFactory;

namespace AccountView.Listener
{
    class Program
    {
        static ManualResetEvent shutdown = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            int retryStartCount = 0;
            ILog log = null;
            IEventProcessor eventProcessor = null;

            while (retryStartCount < 5)
            {
                try
                {
                    var container = Bootstrapper.Run();

                    log = container.Resolve<ILog>();
                    
                    eventProcessor = container.Resolve<IEventProcessor>();

                    eventProcessor.Connect();

                    eventProcessor.SubscribeAll();

                    shutdown.WaitOne();
                }
                catch (Exception e)
                {
                    log.Error("An unexpected error occurred.", e);
                    retryStartCount++;
                }
                finally
                {
                    eventProcessor?.Disconnect();
                }
            }

            log.Information("Account View Listener has shutdown");
        }        
    }
}