using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public interface IEventStore
    {
        Task<IEnumerable<DomainEvent>> ReadEvents(string aggregateId);

        Task AppendEvents(IEnumerable<DomainEvent> domainEvents, long expectedVersion);

        Task<long> GetLatestVersion(string aggregateId);
    }
}
