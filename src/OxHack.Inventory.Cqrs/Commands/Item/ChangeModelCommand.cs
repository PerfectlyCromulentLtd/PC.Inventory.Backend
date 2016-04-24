using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeModelCommand : ICommand, IConcurrencyAware
    {
        public ChangeModelCommand(Guid aggregateRootId, Guid concurrencyId, string model)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Model = model;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Model
        {
            get;
        }
    }
}
