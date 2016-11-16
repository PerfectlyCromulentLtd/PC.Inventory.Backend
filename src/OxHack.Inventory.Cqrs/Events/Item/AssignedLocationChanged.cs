using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class AssignedLocationChanged : IAggregateEvent
	{
        public AssignedLocationChanged(Guid id, int concurrencyId, string assignedLocation)
        {
            this.Id = id;
            this.ConcurrencyId = concurrencyId;
            this.AssignedLocation = assignedLocation;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
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
