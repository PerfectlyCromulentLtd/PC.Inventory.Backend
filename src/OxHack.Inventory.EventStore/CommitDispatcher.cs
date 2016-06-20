using NEventStore.Dispatcher;
using OxHack.Inventory.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEventStore;
using OxHack.Inventory.Cqrs.Events;

namespace OxHack.Inventory.EventStore
{
    public class CommitDispatcher : IDispatchCommits
	{
		private readonly IBus bus;

		public CommitDispatcher(IBus bus)
		{
			this.bus = bus;
		}

		public void Dispatch(ICommit commit)
		{
			var events = commit.Events.Select(item => item.Body as IEvent).ToList();

			try
			{
				foreach (var @event in events)
				{
					this.bus.RaiseEventAsync(@event).Wait();
				}
			}
			catch
			{
				throw;
			}
		}

		public void Dispose()
		{
			// nothing yet.
		}
	}
}
