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

        private List<DomainEvent<T>> uncommitedEvents = new List<DomainEvent<T>>();

        public IEnumerable<DomainEvent<T>> UncommittedEvents => this.uncommitedEvents.AsReadOnly();

        public Aggregate()
        {

        }

        protected void Apply(DomainEvent<T> @event)
        {
            this.ApplyWhen(@event);
            this.uncommitedEvents.Add(@event);
            this.version++;
        }

        private void ApplyWhen(DomainEvent<T> @event)
        {
            var allMethods = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo info = allMethods.FirstOrDefault(
                m => m.Name == "When" &&
                m.GetParameters().FirstOrDefault().ParameterType == @event.GetType());

            if (info == null) throw new Exception("Could Not Find DomainEvent");//refactor

            info.Invoke(this, new object[] { @event });
        }

        public void LoadFromHistory(IEnumerable<DomainEvent<T>> eventHistory)
        {
            var orderedEvents = eventHistory.OrderBy(x => x.Version);

            foreach (var @event in orderedEvents)
            {
                this.Apply(@event);
            }

            this.ClearUncommittedEvents();
        }

        public void AddEvent(DomainEvent<T> @event) => this.uncommitedEvents.Add(@event);

        public void AddEvents(IEnumerable<DomainEvent<T>> @events)
        {
            foreach (var @event in @events)
            {
                this.AddEvent(@event);
            }
        }

        public void ClearUncommittedEvents() => uncommitedEvents.Clear();

    }
}
