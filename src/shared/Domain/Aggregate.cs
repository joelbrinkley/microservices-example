using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Domain.Aggregates
{
    public class Aggregate<T>
    {
        private long version;
        public long Version => version;

        private List<DomainEvent> uncommitedEvents = new List<DomainEvent>();

        public IEnumerable<DomainEvent> UncommittedEvents => this.uncommitedEvents.AsReadOnly();

        public Aggregate()
        {

        }

        protected void Apply(DomainEvent @event)
        {
            this.ApplyWhen(@event);
            this.AddEvent(@event);
        }

        private void ApplyWhen(DomainEvent @event)
        {
            var allMethods = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo info = allMethods.FirstOrDefault(
                m => m.Name == "When" &&
                m.GetParameters().FirstOrDefault().ParameterType == @event.GetType());

            if (info == null) throw new Exception("Could Not Find DomainEvent");//refactor

            info.Invoke(this, new object[] { @event });
        }

        public void LoadFromHistory(IEnumerable<DomainEvent> eventHistory)
        {
            var orderedEvents = eventHistory.OrderBy(x => x.Version);

            foreach (var @event in orderedEvents)
            {
                this.Apply(@event);
            }

            this.ClearUncommittedEvents();
        }

        public void AddEvent(DomainEvent @event)
        {
            this.uncommitedEvents.Add(@event);
            this.version++;
        }

        public void AddEvents(IEnumerable<DomainEvent> @events)
        {
            foreach (var @event in @events)
            {
                this.AddEvent(@event);
            }
        }

        public void ClearUncommittedEvents() => uncommitedEvents.Clear();

    }
}
