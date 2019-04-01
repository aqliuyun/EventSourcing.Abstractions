using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class AggregateRootStoreManager
    {
        private static Dictionary<string, IAggregateRootStoreProvider> _providers;
        private static readonly ConcurrentDictionary<Type, string> Mappings = new ConcurrentDictionary<Type, string>();
        private static bool _hasDefaultProvider;
        private static string _defaultProviderName = string.Empty;

        public static IAggregateRootStoreProvider GetProvider<T>()
        {
            return GetProvider(typeof(T));
        }

        public static IAggregateRootStoreProvider GetProvider(Type aggregateRootType)
        {
            IAggregateRootStoreProvider provider;
            var providerName = Mappings.GetOrAdd(aggregateRootType, GetProviderName);

            _providers.TryGetValue(providerName, out provider);

            return provider;
        }

        private static string GetProviderName(Type serviceType)
        {
            var attr = serviceType.GetCustomAttribute<AggregateRootStoreAttribute>();

            if (attr == null && !_hasDefaultProvider)
            {
                throw new Exception("Unkown provider");
            }
            return attr == null ? _defaultProviderName : attr.ProviderName;
        }
    }
}
