using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventStoreAttribute : Attribute
    {
        public string ProviderName { get; set; }
    }
}
