using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class DomainService<T> : IDomainService where T:IAggregateRoot
    {        
        private EventCenter<T> _evtCenter { get; set; }

        public IAggregateRoot AggregateRoot { get; protected set; }

        public DomainService(IAggregateRoot root) {
            this.AggregateRoot = root;
        }

        protected Task RaiseEvent(IDomainEvent evt)
        {
            try
            {
                evt.AggregateRootId = this.AggregateRoot.ID();
                evt.Version = this.AggregateRoot.Version + 1;
                evt.UtcTimestamp = DateTime.Now.ToUniversalTime();
                return this._evtCenter.WriteEvent(evt);
            }
            catch (Exception)
            {
                throw;
            }
        }        

        public Task ReplayEvents()
        {
            return Task.Run(async () =>
            {
                this._evtCenter = await EventCenter<T>.Initialize(this, 100);
                await this._evtCenter.ReplayEvents();
            });
        }        
    }
}
