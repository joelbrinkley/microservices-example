﻿using EventListener;
using Logging;
using System;
using System.Threading;
using Autofac;

namespace AccountListener
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
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    //log.Error("An unexpected error occurred.", e);
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
