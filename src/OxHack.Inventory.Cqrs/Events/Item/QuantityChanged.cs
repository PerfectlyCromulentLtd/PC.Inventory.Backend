using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class QuantityChanged : IEvent, IConcurrencyAware
    {

        public QuantityChanged(Guid aggregateRootId, Guid concurrencyId, int quantity)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Quantity = quantity;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public int Quantity
        {
            get;
        }
    }
}
