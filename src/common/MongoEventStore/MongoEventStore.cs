using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using MongoDB.Bson.Serialization;
using EventStore;
using Domain.DomainEvents;

namespace MongoEventStore
{
    public class MongoEventStore : IEventStore
    {
        private readonly IMongoDatabase mongoDatabase;

        public MongoEventStore(IMongoClient mongoClient)
        {
            this.mongoDatabase = mongoClient.GetDatabase("bankAccountDb");
        }

        public void AddEvent<T>(DomainEvent<T> @event)
        {
            throw new NotImplementedException();
        }

        public async Task AppendEvents<T>(IEnumerable<DomainEvent<T>> domainEvents, long exepectedVersion)
        {
            if (!domainEvents.Any()) return; //return if no events are bassed in

            var originalVersion = exepectedVersion - domainEvents.Count() + 1;

            var latestVersion = await GetLatestVersion(domainEvents.First().AggregateId); //get the latest version to check if aggregate has been updated

            if (latestVersion >= originalVersion)
            {
                throw new Exception("Objected out of date");
            }

            var documents = domainEvents.Select(x => x.SerializeEvent());

            await mongoDatabase.GetCollection<BsonDocument>("events").InsertManyAsync(documents);
        }

        public async Task<long> GetLatestVersion(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("AggregateId", id);

            var lastEvent = await mongoDatabase.GetCollection<BsonDocument>("events")
                    .Find(filter)
                    .Sort("{version:-1}")
                    .FirstOrDefaultAsync();

            if (lastEvent == null) return 0;

            return lastEvent["version"].AsInt64;
        }

        public async Task<IEnumerable<DomainEvent<T>>> ReadEvents<T>(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("AggregateId", id);

            var events = await mongoDatabase.GetCollection<BsonDocument>("events").Find(filter).ToListAsync();

            return events.Select(e => e.DeserializeEvent<DomainEvent<T>>());
        }
    }
}
