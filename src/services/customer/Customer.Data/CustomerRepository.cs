using Customers.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoClient client;
        private readonly ILog log;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IMongoCollection<Customer> customerCollection;
        private readonly IMongoCollection<BsonDocument> eventsCollection;

        public CustomerRepository(IMongoClient client, ILog log)
        {
            this.client = client;
            this.log = log;
            this.mongoDatabase = client.GetDatabase("customerDb");
            this.customerCollection = mongoDatabase.GetCollection<Customer>("customers");
            this.eventsCollection = mongoDatabase.GetCollection<BsonDocument>("events");
        }


        public async Task AddOrUpdate(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq("Id", customer.Id);

            var updateId = Builders<Customer>.Update.Set("Id", customer.Id);
            var updateName = Builders<Customer>.Update.Set("Name", customer.Name);
            var updateEmail = Builders<Customer>.Update.Set("EmailAddress", customer.EmailAddress);
            var updateAddress = Builders<Customer>.Update.Set("Address", customer.Address);
            var updateDef = Builders<Customer>.Update.Combine(updateId, updateName, updateEmail, updateAddress);

            var options = new FindOneAndUpdateOptions<Customer>()
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            try
            {
                var result = await customerCollection.FindOneAndUpdateAsync(filter, updateDef, options);

                var events = customer.UncommittedEvents.Select(x => x.ToBsonDocument());

                this.eventsCollection.InsertMany(events);
            }
            catch(Exception e)
            {
                this.log.Error($"An error occurred while adding or updating the customer {customer.Id}, {customer.Name.FullName}");
            }
          
        }

        public async Task<Customer> Find(Guid id)
        {
            var customer = await this.customerCollection.Find<Customer>(x => x.Id == id).FirstAsync();

            if (customer == null)
            {
                this.log.Information($"Unable to find customer with customer id {id}");
            }

            return customer;

        }
    }
}
