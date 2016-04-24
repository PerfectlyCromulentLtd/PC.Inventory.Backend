using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeQuantityCommand : ICommand, IConcurrencyAware
    {
        public ChangeQuantityCommand(Guid aggregateRootId, Guid concurrencyId, int quantity)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Quantity = quantity;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public int Quantity
        {
            get;
        }
    }
}
