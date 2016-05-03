using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class PhotoAdded : IEvent
    {
        public PhotoAdded(Guid aggregateRootId, string photoFilename)
        {
            this.AggregateRootId = aggregateRootId;
            this.PhotoFilename = photoFilename;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public string PhotoFilename
        {
            get;
        }
    }
}
