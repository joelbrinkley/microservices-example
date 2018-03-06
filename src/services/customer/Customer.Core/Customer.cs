using Domain.Aggregates;
using System;
using Customers.Events;

namespace Customers
{
    public class Customer : Aggregate<Customer>
    {
        public string Id { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public EmailAddress EmailAddress { get;  set; }
        public bool IsPreferredCustomer { get;  set; }

        public Customer()
        {

        }      

        public static Customer Create(Name name, Address address, EmailAddress email, bool isPreferredCustomer = false)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Address = address,
                EmailAddress = email,
                IsPreferredCustomer = isPreferredCustomer
            };

            customer.AddEvent(new NewCustomerCreated(customer));
           
            return customer;
        }
    }
}
