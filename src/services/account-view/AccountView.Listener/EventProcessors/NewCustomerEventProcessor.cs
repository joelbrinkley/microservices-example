using System;
using System.Collections.Generic;
using System.Text;
using Domain.DomainEvents;
using AccountView.Data;
using AccountView.Listener.ContextFactory;
using Newtonsoft.Json;
using Logging;

namespace AccountView.Listener.EventProcessors
{
    public class NewCustomerEventProcessor : IProcessEvents
    {
        private readonly IAccountViewContextFactory contextFactory;

        public NewCustomerEventProcessor(IAccountViewContextFactory contextFactory, ILog log)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(DomainEvent<object> @event)
        {
            using(var context = this.contextFactory.GetContext())
            {
                var jobject = JsonConvert.DeserializeObject(@event.EventData);
            }
        }
    }
}
