using Customers.Events;
using Customers.Queries;
using Domain;
using Domain.DomainEvents;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Application.Queries
{
    public class GetCustomerEventStreamQueryHandler : IQueryHandler<GetCustomerEventStream, IEnumerable<DomainEvent<Customer>>>
    {
        private readonly IEventStream<Customer> eventStream;

        public GetCustomerEventStreamQueryHandler(IEventStream<Customer> eventStream)
        {
            this.eventStream = eventStream;
        }

        public async Task<IEnumerable<DomainEvent<Customer>>> Execute(GetCustomerEventStream query)
        {
            return await eventStream.ReadAll();
        }
    }
}
