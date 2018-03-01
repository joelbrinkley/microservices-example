
using Autofac;
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

      

            return builder.Build();
        }
    }
}
