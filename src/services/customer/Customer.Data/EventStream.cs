using Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.DomainEvents;
using System.Threading.Tasks;
using Logging;
using MongoDB.Bson;
using System.Linq;

namespace Customers
{
    public class EventStream : IEventStream<Customer>
    {
        private readonly IMongoClient mongoClient;
        private readonly ILog log;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IMongoCollection<DomainEvent<Customer>> eventsCollection;

        public EventStream(IMongoClient client, ILog log)
        {
            this.log = log;
            this.mongoDatabase = client.GetDatabase("customerDb");
            this.eventsCollection = mongoDatabase.GetCollection<DomainEvent<Customer>>("events");
        }

        public async Task<IEnumerable<DomainEvent<Customer>>> ReadAll()
        {
            log.Information("Reading Event Stream for Customers");

            var events = await this.eventsCollection.Find(Builders<DomainEvent<Customer>>.Filter.Empty).ToListAsync();

            if (events == null || !events.Any())
            {
                this.log.Information($"No events in the customer event stream");
            }

            return events;

        }
    }
}
