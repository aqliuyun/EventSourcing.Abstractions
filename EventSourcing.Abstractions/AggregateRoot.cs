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

        private EventCenter mailbox;

        public AggregateRoot()
        {
            Task.Run(async() => { mailbox = new EventCenter(); await mailbox.Initialize(this); });
        }        

        public string ID()
        {
            return UniqueID.ToString();
        }

        public Task RaiseEvent(IDomainEvent evt)
        {
            evt.AggregateRootId = this.ID();
            evt.Version = this.Version + 1;
            evt.UtcTimestamp = DateTime.Now.ToUniversalTime();
            return mailbox.WriteEvent(evt);
        }

        public Task ReplayEvents()
        {           
            return mailbox.ReplayEvents();
        }
    }
}
