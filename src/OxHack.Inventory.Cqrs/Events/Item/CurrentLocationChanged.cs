using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class CurrentLocationChanged : IEvent, IConcurrencyAware
    {
        public CurrentLocationChanged(Guid aggregateRootId, Guid concurrencyId, string currentLocation)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.CurrentLocation = currentLocation;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string CurrentLocation
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.CurrentLocation = this.CurrentLocation;

            return aggregate;
        }
    }
}
