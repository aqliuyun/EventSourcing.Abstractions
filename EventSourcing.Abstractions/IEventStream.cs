using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public interface IEventStream
    {
        Task<bool> Write(IDomainEvent @event);
    }
}
