using Account;
using Domain.DomainEvents;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account
{
    public static class BsonClassMappings
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<DomainEvent>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.AggregateType).SetElementName("AggregateType");
                cm.MapCreator(x => new DomainEvent(x.Id.ToString(), x.AggregateId, x.AggregateType, x.MessageNameSpace, x.CreatedOn, x.HasBeenPublished, x.EventData));
            });
        }
    }
}
