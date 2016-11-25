using OxHack.Inventory.Cqrs.Events;
using System;
using System.Collections.Generic;

namespace OxHack.Inventory.Cqrs
{
    public interface IEventStore
    {
        void StoreAggregateEvent(IAggregateEvent @event, dynamic eventMetadata);
		void StoreEvent(string streamName, IEvent @event, dynamic eventMetadata);
		IReadOnlyList<StoredEvent> GetAllEvents();
		IReadOnlyList<StoredEvent> GetEventsByAggregateId(Guid aggregateId);
	}
}