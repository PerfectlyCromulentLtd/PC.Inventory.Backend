using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeNameCommand : ICommand, IConcurrencyAware, IMapToEvent<NameChanged>
    {
        public ChangeNameCommand(Guid aggregateRootId, Guid concurrencyId, string name)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Name = name;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Name
        {
            get;
        }

        public NameChanged GetEvent()
        {
            return new NameChanged(this.AggregateRootId, this.ConcurrencyId, this.Name);
        }
    }
}
