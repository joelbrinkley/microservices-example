using Customers.Events;
using Domain.DomainEvents;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers
{
    public static class BsonClassMappings
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Name>(cm =>
            {
                cm.MapProperty(x => x.First);
                cm.MapProperty(x => x.Last);
                cm.MapProperty(x => x.Middle);
                cm.MapCreator(x => new Name(x.First, x.Middle, x.Last));
            });


            BsonClassMap.RegisterClassMap<EmailAddress>(cm =>
            {
                cm.MapProperty(x => x.Value);
                cm.MapCreator(x => x.Value);
            });


            BsonClassMap.RegisterClassMap<Customer>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id);
            });

            BsonClassMap.RegisterClassMap<DomainEvent<Customer>>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.AggregateType).SetElementName("AggregateType");
            });

            BsonClassMap.RegisterClassMap<NewCustomerCreated>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.Customer).SetElementName("Customer");
            });
        }
    }
}
