using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeOriginCommand : ICommand, IConcurrencyAware, IMapToEvent<OriginChanged>
    {
        public ChangeOriginCommand(Guid aggregateRootId, int concurrencyId, string origin)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Origin = origin;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Origin
        {
            get;
        }

        public OriginChanged GetEvent()
        {
            return new OriginChanged(this.AggregateRootId, this.ConcurrencyId + 1, this.Origin);
        }
    }
}
