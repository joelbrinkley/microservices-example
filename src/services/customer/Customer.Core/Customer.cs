using Domain.Aggregates;
using System;
using Customers.Events;

namespace Customers
{
    public class Customer : Aggregate<Customer>
    {
        public Guid Id { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public EmailAddress EmailAddress { get; set; }

        public Customer()
        {

        }      

        public static Customer Create(Name name, Address address, EmailAddress email)
        {
            
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Address = address,
                EmailAddress = email
            };

            customer.AddEvent(new NewCustomerCreated(customer));

            return customer;
        }
    }
}
