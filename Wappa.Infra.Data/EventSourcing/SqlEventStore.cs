using Wappa.Domain.Core.Events;
using Wappa.Domain.Interfaces;
using Wappa.Infra.Data.Repository.EventSourcing;
using Newtonsoft.Json;

namespace Wappa.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public SqlEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;            
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                "SysAdmin");

            _eventStoreRepository.Store(storedEvent);
        }
    }
}