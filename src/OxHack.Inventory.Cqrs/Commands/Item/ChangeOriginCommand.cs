using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeOriginCommand : ICommand, IConcurrencyAware, IMapToEvent<OriginChanged>
    {
        public ChangeOriginCommand(Guid aggregateRootId, int concurrencyId, string origin, dynamic issuerMetadata)
		{
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Origin = origin;
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

        public string Origin
        {
            get;
		}

		public dynamic IssuerMetadata
		{
			get;
		}

		public OriginChanged GetEvent()
        {
            return new OriginChanged(this.Id, this.ConcurrencyId + 1, this.Origin);
        }
    }
}
