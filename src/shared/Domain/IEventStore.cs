using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public interface IEventStore
    {
        Task<IEnumerable<DomainEvent<T>>> ReadEvents<T>(string aggregateId);

        Task AppendEvents<T>(IEnumerable<DomainEvent<T>> domainEvents, long expectedVersion);

        Task<long> GetLatestVersion(string aggregateId);
    }
}
