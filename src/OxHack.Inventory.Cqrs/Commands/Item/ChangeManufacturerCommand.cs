using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeManufacturerCommand : ICommand, IConcurrencyAware
    {
        public ChangeManufacturerCommand(Guid aggregateRootId, Guid concurrencyId, string manufacturer)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Manufacturer = manufacturer;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string Manufacturer
        {
            get;
        }
    }
}
