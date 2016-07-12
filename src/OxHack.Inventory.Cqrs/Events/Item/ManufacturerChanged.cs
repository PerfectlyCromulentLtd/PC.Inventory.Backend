using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class ManufacturerChanged : IEvent, IConcurrencyAware
    {
        public ManufacturerChanged(Guid aggregateRootId, int concurrencyId, string manufacturer)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Manufacturer = manufacturer;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Manufacturer
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.Manufacturer = this.Manufacturer;

            return aggregate;
        }
    }
}
