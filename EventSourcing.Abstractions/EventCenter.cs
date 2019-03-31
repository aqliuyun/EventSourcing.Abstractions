using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class EventCenter<T> where T : IAggregateRoot
    {
        private static readonly IEventStoreProvider EventStoreProvider = EventStoreManager.GetProvider<IDomainService>();
        private long _afterSnapshotsEventCount;
        private IDomainService _service;
        private IEventStore _eventStore;

        private T State
        {
            get
            {
                return (T)this._service.AggregateRoot;
            }
        }

        private EventCenter() { }

        public static async Task<EventCenter<T>> Initialize(IDomainService service, long afterSnapshotsEventCount = 100)
        {
            var instance = new EventCenter<T>
            {
                _service = service,
                _afterSnapshotsEventCount = afterSnapshotsEventCount,
                _eventStore = await EventStoreProvider.Create<IDomainService>()
            };

            return instance;
        }
        public async Task WriteEvent(IDomainEvent evt)
        {
            if (evt != null)
            {
                var writeResult = await this._eventStore.AppendAsync(evt);
                
                if (writeResult == EventWriteResult.Success)
                {
                    HandleEvent(evt);
                }

                else if (writeResult == EventWriteResult.Duplicate)
                {
                    evt = await this._eventStore.ReadOneAsync(this._service.AggregateRoot.ID(), evt.CommandId);
                }

                else
                    throw new Exception("event store write exception");


                if (await EventStreamManager.GetStream().Write(evt))
                {
                    if (evt.Version % this._afterSnapshotsEventCount == 0)
                        await this.WriteSnapshot();
                }
                else
                {
                    throw new Exception("publish event stream exception");
                }
            }
        }

        private Task WriteSnapshot()
        {
            return this._service.AggregateRoot.CommitChanges();
        }

        public async Task ReplayEvents()
        {
            var events = await this._eventStore.ReadFromAsync(this._service.AggregateRoot.ID(), this._service.AggregateRoot.Version + 1);

            if (events.Any())
            {
                events = events.OrderBy(e => e.Version);

                foreach (var evt in events)
                {
                    HandleEvent(evt);
                }
            }
        }

        private void HandleEvent(IDomainEvent evt)
        {
            VerifyEvent(evt);
            evt.Apply(this.State);
            this.State.Version = evt.Version;
        }

        private void VerifyEvent(IDomainEvent evt)
        {
            if (evt.Version != this.State.Version + 1)
            {
                throw new Exception($"invlid event version for [{evt.GetType().FullName}] of [{this.GetType().FullName}]");
            }
        }
    }
}
