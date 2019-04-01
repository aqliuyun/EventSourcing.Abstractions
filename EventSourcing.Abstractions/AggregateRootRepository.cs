using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class AggregateRootRepository : IAggregateRootRepository
    {
        private IAggregateRootStore storage;
        private ConcurrentDictionary<string, IAggregateRoot> cache = new ConcurrentDictionary<string, IAggregateRoot>();

        public AggregateRootRepository(IAggregateRootStoreProvider _provider)
        {
            this.storage = _provider.GetStorage();
        }

        public T CreateOne<T>() where T : IAggregateRoot,new()
        {
            var t = new T();            
            storage.Save(t);
            cache.TryAdd(t.ID(), t);
            return t;
        }

        public T Find<T>(string id) where T : IAggregateRoot, new()
        {
            if(cache.TryGetValue(id,out IAggregateRoot value))
            {
                return (T)value;
            }
            return default(T);
        }        
    }
}
