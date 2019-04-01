using EventSourcing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferDemo
{
    public class MemoryStorage : IAggregateRootStore
    {
        public Task Save(IAggregateRoot aggregateRoot)
        {
            return Task.CompletedTask;
        }
    }

    public class MemoryStorageProvider : IAggregateRootStoreProvider
    {
        public IAggregateRootStore GetStorage()
        {
            IAggregateRootStore store =  new MemoryStorage();
            return store;
        }
    }
}
