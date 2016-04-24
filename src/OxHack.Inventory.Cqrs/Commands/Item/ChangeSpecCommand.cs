using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeSpecCommand : ICommand, IConcurrencyAware
    {
        public ChangeSpecCommand(Guid aggregateRootId, Guid concurrencyId, string spec) 
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Spec = spec;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Spec
        {
            get;
        }
    }
}
