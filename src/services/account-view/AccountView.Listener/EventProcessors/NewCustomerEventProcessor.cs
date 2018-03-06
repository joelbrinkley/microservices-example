using System;
using System.Collections.Generic;
using System.Text;
using Domain.DomainEvents;
using AccountView.Data;
using AccountView.Listener.ContextFactory;
using Newtonsoft.Json;
using Logging;
using NATS.Client;
using Newtonsoft.Json.Linq;
using EventListener;

namespace AccountView.Listener.EventProcessors
{
    public class NewCustomerEventProcessor : IProcessEvents
    {
        private readonly IAccountViewContextFactory contextFactory;
        private readonly ILog log;
        public string QueuePrefix => "Account.View";
        
        public NewCustomerEventProcessor(IAccountViewContextFactory contextFactory, ILog log)
        {
            this.contextFactory = contextFactory;
            this.log = log;
        }

        public void HandleSubscription(object sender, MsgHandlerEventArgs e)
        {
            try
            {
                var message = MessageSerializer.Deserializer<DomainEvent>(e.Message.Data);
                Process(message);
                log.Information("Processed New Customer Event", message);
            }
            catch(Exception ex)
            {
                log.Information("Unabled to process New Customer Event", MessageSerializer.Deserializer<DomainEvent>(e.Message.Data));
                log.Error("Error", ex);
                throw ex;
            }
        
        }

        public void Process(DomainEvent @event)
        {
            using(var context = this.contextFactory.GetContext())
            {
                var jobject = JsonConvert.DeserializeObject<JObject>(@event.EventData.ToString());

                var customer = new Customer()
                {
                    CustomerObjectId = new Guid(jobject["Id"]?.ToString()),
                    FirstName = jobject["Name"]["First"].ToString(),
                    LastName = jobject["Name"]["Last"].ToString(),
                    MiddleName = jobject["Name"]["Middle"].ToString()
                };

                context.Customers.Add(customer);

                context.SaveChanges();
            }
        }
    }
}
