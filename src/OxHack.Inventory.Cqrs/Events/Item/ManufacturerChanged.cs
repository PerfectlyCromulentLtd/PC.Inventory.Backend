using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class ManufacturerChanged : IAggregateEvent
	{
        public ManufacturerChanged(Guid id, int concurrencyId, string manufacturer)
        {
            this.Id = id;
            this.ConcurrencyId = concurrencyId;
            this.Manufacturer = manufacturer;
        }

        public Guid Id
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
