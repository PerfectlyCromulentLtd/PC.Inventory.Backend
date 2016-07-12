using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeCategoryCommand : ICommand, IConcurrencyAware, IMapToEvent<CategoryChanged>
    {
        public ChangeCategoryCommand(Guid aggregateRootId, int concurrencyId, string category)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Category = category;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Category
        {
            get;
        }

        public CategoryChanged GetEvent()
        {
            return new CategoryChanged(this.AggregateRootId, this.ConcurrencyId, this.Category);
        }
    }
}
