using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public interface IEventStore
    {
        Task<EventWriteResult> AppendAsync(IDomainEvent @event);
        Task<IEnumerable<IDomainEvent>> ReadFromAsync(string aggregateRootId, long eventVersion = 0);
        Task<IDomainEvent> ReadOneAsync(string aggregateRootId, string commandId);
    }

    public enum EventWriteResult
    {
        Success = 1,
        Duplicate = 2,
        UnknowError = 3
    }
}
