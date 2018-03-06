using Domain.DomainEvents;
using Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using NATS.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MongoEventStorePublisher
{
    public class PublisherService
    {
        private readonly IMongoClient mongoClient;
        private readonly string natsConnection;
        private readonly ILog log;
        private bool running = false;

        public PublisherService(IMongoClient mongoClient, string natsConnection, ILog log)
        {
            this.mongoClient = mongoClient;
            this.natsConnection = natsConnection;
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

        public IEnumerable<DomainEvent> GetAllUnprocessedEvents()
        {
            var eventCollection = mongoClient.GetDatabase("eventStoreDb").GetCollection<DomainEvent>("events");

            var filter = Builders<DomainEvent>.Filter.Eq("HasBeenPublished", false);

            var unpublishedEvents = eventCollection.Find(filter).ToList();

            return unpublishedEvents;

        }

        public void MarkEventsAsProcessed(IEnumerable<DomainEvent> events)
        {
            if (!events.Any()) return;
            var eventCollection = mongoClient.GetDatabase("eventStoreDb").GetCollection<DomainEvent>("events");

            var ids = events.Where(x => x.HasBeenPublished)
                            .Select(x => x.Id)
                            .ToList();

            var filter = Builders<DomainEvent>.Filter.In("Id", ids);
            var updateIsPublished = Builders<DomainEvent>.Update.Set("HasBeenPublished", true);
            var updatePublishedOn = Builders<DomainEvent>.Update.Set("PublishedOn", DateTime.UtcNow);
            var update = Builders<DomainEvent>.Update.Combine(updateIsPublished, updatePublishedOn);

            eventCollection.UpdateMany(filter, update);

            this.log.Information($"Marked events as published.");
        }

        public void PublishEvents(IEnumerable<DomainEvent> events)
        {
            using (var connection = new ConnectionFactory().CreateConnection(natsConnection))
            {
                foreach (var @event in events)
                {
                    try
                    {
                        var jsonPayload = JsonConvert.SerializeObject(@event);

                        connection.Publish(@event.MessageNameSpace.ToString(), Encoding.UTF8.GetBytes(jsonPayload));

                        this.log.Information($"Published: {jsonPayload}");

                        @event.MarkPublished();
                    }
                    catch (Exception e)
                    {
                        this.log.Error($"Failed to publish event {@event.MessageNameSpace}: {@event.Id} for aggregate {@event.AggregateId}");
                    }
                }

                connection.Close();
            }
        }
    }
}
