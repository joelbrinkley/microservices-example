using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Events
{
    public class NewCustomerCreated : DomainEvent<Customer>
    {
        public const string EVENT_NAMESPACE = "Customer.Created";

        public NewCustomerCreated(Customer customer) : base(customer.Id, EVENT_NAMESPACE)
        {
            Customer = customer;
        }

        public NewCustomerCreated(string id, string aggregateId, DateTime createdOn, bool hasBeenPublished, Customer customer)
             :base(new Guid(id), aggregateId, EVENT_NAMESPACE, createdOn, hasBeenPublished)
        {
            this.Customer = customer;
        }

        public Customer Customer { get; }
    }
}
