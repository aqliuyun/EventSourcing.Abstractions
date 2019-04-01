using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public interface IAggregateRoot
    {
        int Version { get; set; }

        string ID();        
        
        Task RaiseEvent(IDomainEvent evt);
    }

    public interface IAggregateRoot<T> : IAggregateRoot
    {
        T UniqueID { get; set; }
    }
}
