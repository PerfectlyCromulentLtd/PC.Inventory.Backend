using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeAppearanceCommand : ICommand, IConcurrencyAware
    {
        public ChangeAppearanceCommand(Guid aggregateRootId, Guid concurrencyId, string appearance)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Appearance = appearance;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Appearance
        {
            get;
        }
    }
}
