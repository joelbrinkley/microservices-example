using System;
using System.Collections.Generic;
using System.Text;

namespace EventListener
{
    public interface IEventProcessor
    {
        void Connect();
        void SubscribeAll();
        void Disconnect();
    }
}
