using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class RemovePhotoCommand : ICommand, IConcurrencyAware, IMapToEvent<PhotoRemoved>
    {
        public RemovePhotoCommand(Guid aggregateRootId, int concurrencyId, string photo, dynamic issuerMetadata)
		{
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Photo = photo;
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

        public string Photo
        {
            get;
		}

		public dynamic IssuerMetadata
		{
			get;
		}

		public PhotoRemoved GetEvent()
        {
            return new PhotoRemoved(this.Id, this.ConcurrencyId + 1, this.Photo);
        }
    }
}
