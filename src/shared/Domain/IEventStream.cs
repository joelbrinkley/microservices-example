using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEventStream<T>
    {
        Task<IEnumerable<DomainEvent>> ReadAll();
    }
}
