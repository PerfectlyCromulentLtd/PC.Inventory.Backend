using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeAppearanceCommand : ICommand, IConcurrencyAware, IMapToEvent<AppearanceChanged>
    {
        public ChangeAppearanceCommand(Guid aggregateRootId, int concurrencyId, string appearance)
        {
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Appearance = appearance;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Appearance
        {
            get;
        }

        public AppearanceChanged GetEvent()
        {
            return new AppearanceChanged(this.Id, this.ConcurrencyId + 1, this.Appearance);
        }
    }
}
