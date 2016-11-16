using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class NameChanged : IAggregateEvent
	{
        public NameChanged(Guid id, int concurrencyId, string name)
        {
            this.Id = id;
            this.ConcurrencyId = concurrencyId;
            this.Name = name;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Name
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.Name = this.Name;

            return aggregate;
        }
    }
}
