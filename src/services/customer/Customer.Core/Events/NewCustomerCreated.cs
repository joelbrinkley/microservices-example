using Domain.DomainEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Events
{
    public class NewCustomerCreated : DomainEvent
    {
        public const string EVENT_NAMESPACE = "Customer.Created";

        public NewCustomerCreated(Customer customer) : base(customer.Id, typeof(Customer).ToString(), EVENT_NAMESPACE)
        {
            this.EventData = JsonConvert.SerializeObject(customer);
        }

        public NewCustomerCreated(string id, string aggregateId, DateTime createdOn, bool hasBeenPublished, string eventData)
             : base(new Guid(id).ToString(), aggregateId, typeof(Customer).ToString(), EVENT_NAMESPACE, createdOn, hasBeenPublished, eventData)
        {
        }
    }
}
