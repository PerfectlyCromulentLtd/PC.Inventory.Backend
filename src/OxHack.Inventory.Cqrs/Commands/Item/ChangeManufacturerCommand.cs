using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeManufacturerCommand : ICommand, IConcurrencyAware, IMapToEvent<ManufacturerChanged>
    {
        public ChangeManufacturerCommand(Guid aggregateRootId, int concurrencyId, string manufacturer, dynamic issuerMetadata)
		{
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Manufacturer = manufacturer;
			this.IssuerMetadata = issuerMetadata;
		}

        public Guid Id
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

		public dynamic IssuerMetadata
		{
			get;
		}

		public ManufacturerChanged GetEvent()
        {
            return new ManufacturerChanged(this.Id, this.ConcurrencyId + 1, this.Manufacturer);
        }
    }
}
