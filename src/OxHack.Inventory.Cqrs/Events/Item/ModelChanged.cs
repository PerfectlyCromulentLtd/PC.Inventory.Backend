using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class ModelChanged : IEvent, IConcurrencyAware
    {
        public ModelChanged(Guid aggregateRootId, Guid concurrencyId, string model)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Model = model;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Model
        {
            get;
        }
    }
}
