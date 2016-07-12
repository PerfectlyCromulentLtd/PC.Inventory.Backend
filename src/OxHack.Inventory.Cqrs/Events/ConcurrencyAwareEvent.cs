using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events
{
	public class ConcurrencyAwareEvent<TEvent> : IEvent, IConcurrencyAware where TEvent : IEvent
	{
		private readonly TEvent baseEvent;

		public ConcurrencyAwareEvent(TEvent @event, int concurrencyId)
		{
			this.baseEvent = @event;
			this.ConcurrencyId = concurrencyId;
		}

		public Guid AggregateRootId
			=> this.baseEvent.AggregateRootId;

		public int ConcurrencyId
		{
			get;
		}

		public dynamic Apply(dynamic aggregate)
		{
			return this.baseEvent.Apply(aggregate);
		}
	}
}
