using Autofac;
using Customers.Commands;
using Customers.Queries;
using Domain.Commands;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Autofac
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateCustomerCommandHandler>().As<ICommandHandler<CreateCustomer>>();
            builder.RegisterType<FindCustomerQueryHandler>().As<IQueryHandler<FindCustomerQuery, Customer>>();
        }
    }
}
