﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Abstractions
{
    public interface IAggregateRootStore
    {
        Task Save(IAggregateRoot aggregateRoot);
    }
}
