using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class ModelChanged : IEvent, IConcurrencyAware
    {
        public ModelChanged(Guid aggregateRootId, int concurrencyId, string model)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Model = model;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Model
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.Model = this.Model;

            return aggregate;
        }
    }
}
