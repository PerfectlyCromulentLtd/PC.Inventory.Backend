using OxHack.Inventory.Cqrs.Events;
using System;
using System.Collections.Generic;

namespace OxHack.Inventory.Cqrs
{
    public interface IEventStore
    {
        void StoreAggregateEvent(IAggregateEvent message);
		void StoreEvent(string streamName, IEvent message);
		IReadOnlyList<StoredEvent> GetAllEvents();
		IReadOnlyList<StoredEvent> GetEventsByAggregateId(Guid aggregateId);
	}
}