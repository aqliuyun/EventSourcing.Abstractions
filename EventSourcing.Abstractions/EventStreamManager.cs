using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class EventStreamManager
    {
        private static IEventStreamProvider _eventStreamProvider;

        public static void Initailize(IEventStreamProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("_eventStreamProvider", "provider is null");

            _eventStreamProvider = provider;
        }

        public static IEventStream GetStream()
        {
            if (_eventStreamProvider == null)
                throw new Exception("_eventStreamProvider not initialized");

            return _eventStreamProvider.CreateStream();
        }
    }
}
