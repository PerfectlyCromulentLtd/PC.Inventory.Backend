using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class OriginChanged : IEvent
    {
        public OriginChanged(Guid aggregateRootId, int concurrencyId, string origin)
        {
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Origin = origin;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Origin
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.Origin = this.Origin;

            return aggregate;
        }
    }
}
