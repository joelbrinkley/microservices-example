using System;
using System.Collections.Generic;
using System.Text;

namespace AccountView.Listener.EventProcessors
{
    public interface IEventProcessor
    {
        void Connect();
        void SubscribeAll();
        void Disconnect();
    }
}
