using EventListener;
using System;
using System.Collections.Generic;
using System.Text;
using NATS.Client;
using Newtonsoft.Json.Linq;
using Domain.Commands;
using Account.Commands;
using Logging;
using Newtonsoft.Json;
using Domain.DomainEvents;

namespace AccountListener.EventProcessors
{
    public class NewCustomerEventProcessor : IProcessEvents
    {
        private readonly ICommandHandler<CreateAccount> createAccountHandler;
        private readonly ILog log;
        public string QueuePrefix => "Account";

        public NewCustomerEventProcessor(ICommandHandler<CreateAccount> createAccountHandler, ILog log)
        {
            this.createAccountHandler = createAccountHandler;
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
            catch (Exception ex)
            {
                log.Information("Unabled to process New Customer Event", MessageSerializer.Deserializer<DomainEvent>(e.Message.Data));
                log.Error("Error", ex);
                throw ex;
            }
        }

        public void Process(DomainEvent @event)
        {
            var jobject = JsonConvert.DeserializeObject<JObject>(@event.EventData.ToString());

            var createAccount = new CreateAccount()
            {
                CustomerId = new Guid(jobject["Id"]?.ToString()),
                StartingBalance = 0
            };

            this.createAccountHandler.Handle(createAccount).Wait();
        }
    }
}
