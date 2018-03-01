using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Listener.EventProcessors
{
    public interface IProcessEvents
    {
        void Process(DomainEvent<object> @event);
    }
}
