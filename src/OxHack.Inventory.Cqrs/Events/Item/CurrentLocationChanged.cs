using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class CurrentLocationChanged : IAggregateEvent
	{
        public CurrentLocationChanged(Guid id, int concurrencyId, string currentLocation)
        {
            this.Id = id;
            this.ConcurrencyId = concurrencyId;
            this.CurrentLocation = currentLocation;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
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
