using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class SpecChanged : IAggregateEvent
	{
        public SpecChanged(Guid id, int concurrencyId, string spec)
        {
            this.Id = id;
            this.ConcurrencyId = concurrencyId;
            this.Spec = spec;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
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
