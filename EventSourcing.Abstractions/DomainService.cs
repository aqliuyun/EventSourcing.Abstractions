using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public class DomainService : IDomainService
    {
        public IAggregateRootRepository repo { get; set; }

        public DomainService(IAggregateRootRepository _repo)
        {
            this.repo = _repo;
        }
    }
}
