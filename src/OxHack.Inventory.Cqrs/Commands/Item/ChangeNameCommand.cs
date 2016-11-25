using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeNameCommand : ICommand, IConcurrencyAware, IMapToEvent<NameChanged>
    {
        public ChangeNameCommand(Guid aggregateRootId, int concurrencyId, string name, dynamic issuerMetadata)
		{
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Name = name;
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

        public string Name
        {
            get;
		}

		public dynamic IssuerMetadata
		{
			get;
		}

		public NameChanged GetEvent()
        {
            return new NameChanged(this.Id, this.ConcurrencyId + 1, this.Name);
        }
    }
}
