using NATS.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventListener
{
    public interface IProcessEvents
    {
        void Process(JObject @event);
        void HandleSubscription(object sender, MsgHandlerEventArgs e);
    }
}
