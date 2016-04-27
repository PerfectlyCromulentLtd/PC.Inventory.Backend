using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class AppearanceChanged : IEvent, IConcurrencyAware
    {
        public AppearanceChanged(Guid aggregateRootId, Guid concurrencyId, string appearance)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Appearance = appearance;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Appearance
        {
            get;
        }
    }
}
