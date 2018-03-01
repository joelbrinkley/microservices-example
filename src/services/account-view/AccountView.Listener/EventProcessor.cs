using AccountView.Data;
using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Listener
{
    public class EventProcessor
    {
        private readonly AccountViewContext context;
        private readonly IDictionary<string, EventProcessor> eventProcessorMap;

        public EventProcessor(AccountViewContext context, IDictionary<string, EventProcessor> eventProcessorMap)
        {
            this.context = context;
            this.eventProcessorMap = eventProcessorMap;
        }

        public void Process(DomainEvent<object> @event)
        {
            this.eventProcessorMap[@event.MessageNameSpace].Process(@event);
        }
    }
}
