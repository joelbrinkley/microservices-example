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

        public Customer Customer { get; }
    }
}
