using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class RemovePhotoCommand : ICommand, IConcurrencyAware, IMapToEvent<PhotoRemoved>
    {
        public RemovePhotoCommand(Guid aggregateRootId, int concurrencyId, string photo) 
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Photo = photo;
        }

        public Guid AggregateRootId
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

        public PhotoRemoved GetEvent()
        {
            return new PhotoRemoved(this.AggregateRootId, this.ConcurrencyId + 1, this.Photo);
        }
    }
}
