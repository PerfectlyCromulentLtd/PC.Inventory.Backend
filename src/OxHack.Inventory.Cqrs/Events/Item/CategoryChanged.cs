using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class CategoryChanged : IEvent, IConcurrencyAware
    {
        public CategoryChanged(Guid aggregateRootId, int concurrencyId, string category)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Category = category;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Category
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.Category = this.Category;

            return aggregate;
        }
    }
}
