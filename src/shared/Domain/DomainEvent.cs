﻿using Newtonsoft.Json;
using System;

namespace Domain.DomainEvents
{
    public class DomainEvent
    {
        //Version of the aggregate
        public long Version { get; protected set; }

        //An id for the event, generated by the application
        public string Id { get; protected set; }

        //the id of the aggregate.  Typically a GUID but can be other types
        public string AggregateId { get; protected set; }

        //the .net namespace for the aggregate. used for reflection
        public string AggregateType { get; protected set; }

        //time the event was created
        public DateTime CreatedOn { get; protected set; }

        //a string given to the event to define the messaging namespace.
        public string MessageNameSpace { get; protected set; }

        //A boolean representing as the event been successfully published
        public bool HasBeenPublished { get; protected set; }

        public DateTime? PublishedOn { get; protected set; }

        public string EventData { get; protected set; }

        [JsonConstructor]
        public DomainEvent(string id, string aggregateId, string aggregateType, string messageNameSpace, DateTime createdOn, bool hasBeenPublished, string eventData)
        {
            this.CreatedOn = createdOn;
            this.Id = id;
            this.AggregateId = aggregateId;
            this.MessageNameSpace = messageNameSpace;
            this.HasBeenPublished = HasBeenPublished;
            this.CreatedOn = createdOn;
            this.EventData = eventData;
            this.AggregateType = aggregateType;
        }

        public DomainEvent(string aggregateid, string aggregateType, string messageNameSpace)
           : this(Guid.NewGuid().ToString(), aggregateid, aggregateType, messageNameSpace, DateTime.UtcNow, false, string.Empty)
        {

        }
        public void MarkPublished()
        {
            this.HasBeenPublished = true;
            this.PublishedOn = DateTime.UtcNow;
        }
    }
}
