using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeOriginCommand : ICommand, IConcurrencyAware
    {
        public ChangeOriginCommand(Guid aggregateRootId, Guid concurrencyId, string origin)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Origin = origin;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Origin
        {
            get;
        }
    }
}
