using OxHack.Inventory.Cqrs.Events;
using System;
using System.Collections.Generic;

namespace OxHack.Inventory.Cqrs
{
    public interface IEventStore
    {
        void StoreEvent(IEvent message);
		IReadOnlyList<StoredEvent> GetAllEvents();
		IReadOnlyList<StoredEvent> GetEventsByAggregateId(Guid aggregateId);
	}
}