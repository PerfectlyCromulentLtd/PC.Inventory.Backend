using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class ModelChanged : IAggregateEvent
	{
        public ModelChanged(Guid id, int concurrencyId, string model)
        {
            this.Id = id;
            this.ConcurrencyId = concurrencyId;
            this.Model = model;
        }

        public Guid Id
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
