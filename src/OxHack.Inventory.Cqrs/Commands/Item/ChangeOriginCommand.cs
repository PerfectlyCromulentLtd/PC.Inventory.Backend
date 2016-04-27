using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeOriginCommand : ICommand, IConcurrencyAware, IMapToEvent<OriginChanged>
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

        public OriginChanged GetEvent()
        {
            return new OriginChanged(this.AggregateRootId, this.ConcurrencyId, this.Origin);
        }
    }
}
