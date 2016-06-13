using NEventStore;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.EventStore
{
    public class NEventStoreEventStore : IEventStore
    {
        private readonly IStoreEvents eventStore;

        public NEventStoreEventStore(IStoreEvents eventStore)
        {
            this.eventStore = eventStore;
        }

		public void StoreEvent(IEvent message)
        {
            using (var stream = this.eventStore.OpenStream(message.AggregateRootId, message.ConcurrencyId))
            {
                stream.Add(new EventMessage { Body = message });
                stream.CommitChanges(Guid.NewGuid());
            }
		}

		public IReadOnlyList<StoredEvent> GetAllEvents()
		{
			var commits = this.eventStore.Advanced.GetFrom().ToList();

			var events =
				commits
					.SelectMany(commit => commit.Events.Select(@event => new StoredEvent(commit.CheckpointToken, commit.CommitStamp, @event.Body as IEvent)))
					.ToList();

			return events.AsReadOnly();
		}
	}
}
