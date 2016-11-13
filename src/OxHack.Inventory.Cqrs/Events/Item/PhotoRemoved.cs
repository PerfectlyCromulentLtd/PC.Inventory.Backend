using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class PhotoRemoved : IEvent
    {
        public PhotoRemoved(Guid id, int concurrencyId, string photoFilename)
        {
            this.Id = id;
			this.ConcurrencyId = concurrencyId;
            this.PhotoFilename = photoFilename;
        }

        public Guid Id
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
