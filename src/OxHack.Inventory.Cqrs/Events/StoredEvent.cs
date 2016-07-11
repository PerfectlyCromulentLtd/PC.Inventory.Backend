using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events
{
	/// <summary>
	/// Represents an IEvent that has been stored to the event store.
	/// It contains metadata about the IEvent and also the IEvent itself.
	/// </summary>
    public class StoredEvent
    {
		public StoredEvent(string checkpointToken, DateTime commitStamp, IEvent @event)
		{
			this.CheckpointToken = checkpointToken;
			this.CommitStamp = commitStamp;
			this.Event = @event;
		}

		public string CheckpointToken
		{
			get;
		}
		
		public DateTime CommitStamp
		{
			get;
		}

		public IEvent Event
		{
			get;
		}
    }
}
