using Domain.DomainEvents;
using NATS.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Listener.EventProcessors
{
    public interface IProcessEvents
    {
        void Process(JObject @event);
        void HandleSubscription(object sender, MsgHandlerEventArgs e);
    }
}
