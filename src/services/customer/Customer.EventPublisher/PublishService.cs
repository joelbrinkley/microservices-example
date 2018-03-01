using Customers.Logging;
using Domain.DomainEvents;
using MongoDB.Bson;
using MongoDB.Driver;
using NATS.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Customers.EventPublisher
{
    public class PublisherService<T>
    {
        private readonly IMongoClient mongoClient;
        private readonly ILog log;
        private bool running = false;

        public PublisherService(IMongoClient mongoClient, ILog log)
        {
            this.mongoClient = mongoClient;
            this.log = log;
        }

        public void Start()
        {
            this.log.Information("Starting Event Publisher");

            this.running = true;

            while (running)
            {
                var events = GetAllUnprocessedEvents();
                PublishEvents(events);
                MarkEventsAsProcessed(events);

                Thread.Sleep(500);
            }
        }

        public void Stop()
        {
            this.running = false;
            this.log.Information("Stopping event processor");
        }

        public IEnumerable<DomainEvent<T>> GetAllUnprocessedEvents()
        {
            var eventCollection = mongoClient.GetDatabase("customerDb").GetCollection<DomainEvent<T>>("events");

            var filter = Builders<DomainEvent<T>>.Filter.Where(x => x.HasBeenPublished == false);

            var unpublishedEvents = eventCollection.Find(filter).ToList();

            return unpublishedEvents;

        }

        public void MarkEventsAsProcessed(IEnumerable<DomainEvent<T>> events)
        {
            if (!events.Any()) return;
            var eventCollection = mongoClient.GetDatabase("customerDb").GetCollection<DomainEvent<T>>("events");

            var filter = Builders<DomainEvent<T>>.Filter.In(x => x.Id, events.Where(x => x.HasBeenPublished).Select(x => x.Id));
            var updateIsPublished = Builders<DomainEvent<T>>.Update.Set(x => x.HasBeenPublished, true);
            var updatePublishedOn = Builders<DomainEvent<T>>.Update.Set(x => x.PublishedOn, DateTime.UtcNow);
            var update = Builders<DomainEvent<T>>.Update.Combine(updateIsPublished, updatePublishedOn);

            eventCollection.UpdateMany(filter, update);

            this.log.Information($"Marked events as published.");
        }

        public void PublishEvents(IEnumerable<DomainEvent<T>> events)
        {
            if (!events.Any()) return;



            using (var connection = new ConnectionFactory().CreateConnection(Config.BROKER_URL))
            {
                foreach (var @event in events)
                {
                    try
                    {
                        var jsonPayload = JsonConvert.SerializeObject(@event);
                        connection.Publish(@event.MessageNameSpace, Encoding.UTF8.GetBytes(jsonPayload));

                        this.log.Information($"Published: {jsonPayload}");

                        @event.MarkPublished();
                    }
                    catch (Exception e)
                    {
                        this.log.Error($"Failed to publish event {@event.MessageNameSpace}: {@event.Id} for aggregate {@event.AggregateId}");
                    }
                }
            }
        }
    }
}
