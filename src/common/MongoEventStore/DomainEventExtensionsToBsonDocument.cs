using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoEventStore
{
    public static class DomainEventExtensionsToBsonDocument
    {
        public static BsonDocument SerializeEvent<T>(this T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var bsonDocument = json.ToBsonDocument();
            return bsonDocument;
        }
    }
}
