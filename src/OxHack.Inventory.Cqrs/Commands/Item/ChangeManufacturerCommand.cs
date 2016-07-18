using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeManufacturerCommand : ICommand, IConcurrencyAware, IMapToEvent<ManufacturerChanged>
    {
        public ChangeManufacturerCommand(Guid aggregateRootId, int concurrencyId, string manufacturer)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Manufacturer = manufacturer;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string Manufacturer
        {
            get;
        }

        public ManufacturerChanged GetEvent()
        {
            return new ManufacturerChanged(this.AggregateRootId, this.ConcurrencyId + 1, this.Manufacturer);
        }
    }
}
