using NEventStore;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Cqrs.Exceptions;
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
			try
			{
				using (var stream = this.eventStore.OpenStream(message.Id, message.ConcurrencyId - 1))
				{
					stream.Add(new EventMessage { Body = message });
					stream.CommitChanges(new Guid(message.ConcurrencyId, 0, 0, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }));
				}
			}
			catch (DuplicateCommitException e)
			{
				throw new OptimisticConcurrencyException("Unable to store event.  A duplicate event already exists.", e);
			}
			catch (ConcurrencyException e)
			{
				throw new OptimisticConcurrencyException("Unable to store event.", e);
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

		public IReadOnlyList<StoredEvent> GetEventsByAggregateId(Guid aggregateId)
		{
			var commits = this.eventStore.Advanced.GetFrom(aggregateId.ToString(), int.MinValue, int.MaxValue).ToList();

			var events =
				commits
					.SelectMany(commit => commit.Events.Select(@event => new StoredEvent(commit.CheckpointToken, commit.CommitStamp, @event.Body as IEvent)))
					.ToList();

			return events.AsReadOnly();
		}
	}
}
