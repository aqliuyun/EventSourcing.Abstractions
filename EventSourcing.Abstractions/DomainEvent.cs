using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    [Serializable]
    public class DomainEvent : IDomainEvent
    {
        public string AggregateRootId { get; set; }
        public string CommandId { get; set; }
        public DateTime UtcTimestamp { get; set; }
        public int Version { get; set; }

        public virtual void Apply<T>(T state)
        {
            throw new NotImplementedException();
        }
    }
}
