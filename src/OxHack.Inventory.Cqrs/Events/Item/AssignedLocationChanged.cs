using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class AssignedLocationChanged : IEvent, IConcurrencyAware
    {
        public AssignedLocationChanged(Guid aggregateRootId, Guid concurrencyId, string assignedLocation)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.AssignedLocation = assignedLocation;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string AssignedLocation
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.AssignedLocation = this.AssignedLocation;

            return aggregate;
        }
    }
}
