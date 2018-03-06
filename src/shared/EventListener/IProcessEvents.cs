using Domain.DomainEvents;
using NATS.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventListener
{
    public interface IProcessEvents
    {
        void Process(DomainEvent @event);
        void HandleSubscription(object sender, MsgHandlerEventArgs e);
        string QueuePrefix { get; }
    }
}
