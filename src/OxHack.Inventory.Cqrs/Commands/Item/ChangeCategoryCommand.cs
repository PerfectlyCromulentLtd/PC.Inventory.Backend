using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeCategoryCommand : ICommand, IConcurrencyAware
    {
        public ChangeCategoryCommand(Guid aggregateRootId, Guid concurrencyId, string category)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Category = category;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Category
        {
            get;
        }
    }
}
