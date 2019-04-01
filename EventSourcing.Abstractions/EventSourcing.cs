using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class EventSourcing
    {
        private static IServiceCollection collection;
        private static IServiceProvider provider;
        public static IServiceProvider Intergration(Action<IServiceCollection> configuration)
        {
            collection = new ServiceCollection();
            collection.AddSingleton<IAggregateRootRepository, AggregateRootRepository>();
            configuration?.Invoke(collection);            
            provider = collection.BuildServiceProvider();
            return provider;
        }
    }
}
