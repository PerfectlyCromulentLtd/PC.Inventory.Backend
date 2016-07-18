using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class PhotoRemoved : IEvent
    {
        public PhotoRemoved(Guid aggregateRootId, int concurrencyId, string photoFilename)
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
            photos.Remove(this.PhotoFilename);

            return aggregate;
        }
    }
}
