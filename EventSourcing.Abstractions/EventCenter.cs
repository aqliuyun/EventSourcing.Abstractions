using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class EventCenter
    {        
        private long _afterSnapshotsEventCount;
        private IEventStore _eventStore;
        private IAggregateRoot _aggregateRoot;
        private IAggregateRootStore _aggregateStore;
        public EventCenter() { }

        public async Task<EventCenter> Initialize(IAggregateRoot root, long afterSnapshotsEventCount = 100)
        {            
            var instance = new EventCenter
            {
                _aggregateRoot = root,
                _afterSnapshotsEventCount = afterSnapshotsEventCount,
                _eventStore = EventStoreManager.GetProvider(this._aggregateRoot.GetType()).GetStorage(),
                _aggregateStore = AggregateRootStoreManager.GetProvider(this._aggregateRoot.GetType()).GetStorage()
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
                    evt = await this._eventStore.ReadOneAsync(this._aggregateRoot.ID(), evt.CommandId);
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
            return _aggregateStore.Save(this._aggregateRoot);
        }

        public async Task ReplayEvents()
        {
            var events = await this._eventStore.ReadFromAsync(this._aggregateRoot.ID(), this._aggregateRoot.Version + 1);

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
            evt.Apply(this._aggregateRoot);
            this._aggregateRoot.Version = evt.Version;
        }

        private void VerifyEvent(IDomainEvent evt)
        {
            if (evt.Version != this._aggregateRoot.Version + 1)
            {
                throw new Exception($"invlid event version for [{evt.GetType().FullName}] of [{this.GetType().FullName}]");
            }
        }
    }
}
