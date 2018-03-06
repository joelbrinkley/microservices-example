
using Logging;
using NATS.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventListener
{
    public class EventProcessor : IEventProcessor
    {
        private readonly string brokerUrl;
        private readonly EventProcessorMap eventProcessorMap;
        private readonly ILog log;
        private bool isRunning = false;
        private IConnection connection;
        private List<IAsyncSubscription> subscriptions;

        public EventProcessor(string brokerUrl, EventProcessorMap eventProcessorMap, ILog log)
        {
            this.brokerUrl = brokerUrl;
            this.eventProcessorMap = eventProcessorMap;
            this.log = log;
            this.subscriptions = new List<IAsyncSubscription>();
        }

        public void Connect()
        {
            connection = new ConnectionFactory().CreateConnection(brokerUrl);
        }

        public void SubscribeAll()
        {
            this.isRunning = true;

            foreach (var topicName in eventProcessorMap.Keys)
            {
                Subscribe(topicName, eventProcessorMap[topicName]);
            }
        }

        public void Disconnect()
        {
            this.isRunning = false;
            if (connection != null)
            {
                this.connection.Close();

                this.connection = null;

                this.log.Information("Closed the event processor nats connection.");
            }
        }

        public void Subscribe(string topicName, IProcessEvents eventProcessor)
        {
            var queueName = $"{eventProcessor.QueuePrefix}.{topicName}";

            var subscription = connection.SubscribeAsync(topicName, queueName);

            subscription.MessageHandler += eventProcessor.HandleSubscription;

            subscription.Start();

            subscriptions.Add(subscription);

            this.log.Information($"Started subscription for {topicName} using queue {queueName}");
        }



    }
}
