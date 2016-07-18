using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeQuantityCommand : ICommand, IConcurrencyAware, IMapToEvent<QuantityChanged>
    {
        public ChangeQuantityCommand(Guid aggregateRootId, int concurrencyId, int quantity)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Quantity = quantity;
        }

        public Guid AggregateRootId
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

        public QuantityChanged GetEvent()
        {
            return new QuantityChanged(this.AggregateRootId, this.ConcurrencyId + 1, this.Quantity);
        }
    }
}
