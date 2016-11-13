using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class AppearanceChanged : IEvent, IConcurrencyAware
    {
        public AppearanceChanged(Guid id, int concurrencyId, string appearance)
        {
            this.Id = id;
            this.ConcurrencyId = concurrencyId;
            this.Appearance = appearance;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Appearance
        {
            get;
        }

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.Appearance = this.Appearance;

            return aggregate;
        }
    }
}
