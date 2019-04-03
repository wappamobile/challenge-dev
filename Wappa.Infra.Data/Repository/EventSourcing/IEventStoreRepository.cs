using System;
using System.Collections.Generic;
using Wappa.Domain.Core.Events;

namespace Wappa.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}