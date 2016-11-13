using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class QuantityChanged : IEvent
    {
        public QuantityChanged(Guid id, int concurrencyId, int quantity)
        {
            this.Id = id;
            this.ConcurrencyId = concurrencyId;
            this.Quantity = quantity;
        }

		public Guid Id
		{
			get;
		}

        public int ConcurrencyId
        {
            get;
        }

		public int Quantity
		{
			get;
		}

		public dynamic Apply(dynamic aggregate)
		{
			aggregate.Quantity = this.Quantity;

			return aggregate;
		}
	}
}
