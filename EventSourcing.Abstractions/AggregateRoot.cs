using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class AggregateRoot<T> : IAggregateRoot<T>
    {
        public T UniqueID { get; set; }
        public int Version { get; set; }

        public Task CommitChanges()
        {
            throw new NotImplementedException();
        }

        public string ID()
        {
            return UniqueID.ToString();
        }        
    }
}
