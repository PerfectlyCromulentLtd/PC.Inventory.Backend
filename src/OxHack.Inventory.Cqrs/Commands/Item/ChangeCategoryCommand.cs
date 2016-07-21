using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeCategoryCommand : ICommand, IConcurrencyAware, IMapToEvent<CategoryChanged>
    {
        public ChangeCategoryCommand(Guid aggregateRootId, int concurrencyId, string category)
        {
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Category = category;
        }

        public Guid Id
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
            return new CategoryChanged(this.Id, this.ConcurrencyId + 1, this.Category);
        }
    }
}
