using Autofac;
using Customers.Application.Queries;
using Customers.Commands;
using Customers.Queries;
using Domain.Commands;
using Domain.DomainEvents;
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
            builder.RegisterType<GetCustomerEventStreamQueryHandler>().As<IQueryHandler<GetCustomerEventStream, IEnumerable<DomainEvent<Customer>>>>();
        }
    }
}
