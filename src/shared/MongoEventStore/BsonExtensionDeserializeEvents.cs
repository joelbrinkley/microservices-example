using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoEventStore
{
    public static class BsonExtensionDeserializeEvents
    {
        public static T DeserializeEvent<T>(this BsonDocument doc)
        {
            var eventClrTypeName = doc["AggregateType"];
            return (T)JsonConvert.DeserializeObject(doc.ToJson(), Type.GetType((string)eventClrTypeName));
        }
    }
}
