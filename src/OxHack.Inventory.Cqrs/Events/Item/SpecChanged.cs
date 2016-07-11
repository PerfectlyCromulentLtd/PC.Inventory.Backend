using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class SpecChanged : IEvent, IConcurrencyAware
    {
        public SpecChanged(Guid aggregateRootId, Guid concurrencyId, string spec)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Spec = spec;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Spec
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.Spec = this.Spec;

            return aggregate;
        }
    }
}
