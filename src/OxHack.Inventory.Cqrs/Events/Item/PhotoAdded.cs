using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
	public class PhotoAdded : IEvent, IConcurrencyAware
	{
		public PhotoAdded(Guid aggregateRootId, int concurrencyId, string photoFilename)
		{
			this.AggregateRootId = aggregateRootId;
			this.ConcurrencyId = concurrencyId;
			this.PhotoFilename = photoFilename;
		}

		public Guid AggregateRootId
		{
			get;
		}

		public int ConcurrencyId
		{
			get;
		}

		public string PhotoFilename
		{
			get;
		}

		public dynamic Apply(dynamic aggregate)
		{
			List<string> photos = aggregate.Photos;
			photos.Add(this.PhotoFilename);

			return aggregate;
		}
	}
}
