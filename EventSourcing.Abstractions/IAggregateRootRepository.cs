using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public interface IAggregateRootRepository
    {
        T CreateOne<T>() where T : IAggregateRoot, new();

        T Find<T>(string id) where T:IAggregateRoot,new();
    }
}
