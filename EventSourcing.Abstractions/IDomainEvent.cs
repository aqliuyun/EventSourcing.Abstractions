using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public interface IDomainEvent
    {
        string AggregateRootId { get; set; }
        string CommandId { get; set; }
        int Version { get; set; }
        DateTime UtcTimestamp { get; set; }

        void Apply<T>(T state);
    }
}
