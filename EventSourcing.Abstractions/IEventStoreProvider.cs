using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public interface IEventStoreProvider
    {
        Task Initialize(EventStoreProviderSetting settings);

        Task<IEventStore> Create<T>() where T : IDomainService;
    }
}
