using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public interface IAggregateRoot
    {
        string ID();
        int Version { get; set; }
        
        Task CommitChanges();
    }

    public interface IAggregateRoot<T> : IAggregateRoot
    {
        T UniqueID { get; set; }
    }
}
