using NATS.Client;
using System;
using System.Threading;
using Domain.DomainEvents;
using Autofac;
using Logging;
using Logging;

namespace Customers.EventPublisher
{
    class Program
    {
        static ManualResetEvent shutdown = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            int retryStartCount = 0;

            var container = Bootstrapper.Run();

            ILog log = container.Resolve<ILog>();

            var eventPublisher = container.Resolve<PublisherService<Customer>>();

            while (retryStartCount < 5)
            {
                if (retryStartCount != 0)
                {
                    log.Information($"Attempting to restart the event publisher. Retry attempt: {retryStartCount}");
                }

                try
                {
                    eventPublisher.Start();
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    retryStartCount++;
                }
            }

            log.Information("Encountered to many errors: shutting down Event Publisher Service");

            shutdown.WaitOne();

            eventPublisher.Stop();
        }
    }
}
