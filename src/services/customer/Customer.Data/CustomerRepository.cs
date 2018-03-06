using EventStore;
using Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoClient client;
        private readonly IEventStore eventStore;
        private readonly ILog log;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IMongoCollection<Customer> customerCollection;

        public CustomerRepository(IMongoClient client, IEventStore eventStore, ILog log)
        {
            this.client = client;
            this.eventStore = eventStore;
            this.log = log;
            this.mongoDatabase = client.GetDatabase("customerDb");
            this.customerCollection = mongoDatabase.GetCollection<Customer>("customers");
        }


        public async Task AddOrUpdate(Customer customer)
        {
            //probably need some kind of transaction here

            var filter = Builders<Customer>.Filter.Eq("Id", customer.Id);
            var updateId = Builders<Customer>.Update.Set("Id", customer.Id);
            var updateName = Builders<Customer>.Update.Set("Name", customer.Name);
            var updateEmail = Builders<Customer>.Update.Set("EmailAddress", customer.EmailAddress);
            var updateAddress = Builders<Customer>.Update.Set("Address", customer.Address);
            var updatePreferred = Builders<Customer>.Update.Set("IsPreferredCustomer", customer.IsPreferredCustomer);

            var updateDef = Builders<Customer>.Update.Combine(updateId, updateName, updateEmail, updateAddress, updatePreferred);

            var options = new FindOneAndUpdateOptions<Customer>()
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            try
            {
                var events = customer.UncommittedEvents;

                await this.eventStore.AppendEvents(events, customer.Version);

                var result = await customerCollection.FindOneAndUpdateAsync(filter, updateDef, options);           
            }
            catch(Exception e)
            {
                this.log.Error($"An error occurred while adding or updating the customer {customer.Id}, {customer.Name.FullName}");
            }
          
        }

        public async Task<Customer> Find(string id)
        {
            var customer = await this.customerCollection.Find<Customer>(x => x.Id == id.ToLower()).FirstAsync();

            if (customer == null)
            {
                this.log.Information($"Unable to find customer with customer id {id}");
            }

            return customer;

        }
    }
}
