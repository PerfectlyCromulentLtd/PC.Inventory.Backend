using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class AddPhotoCommand : ICommand, IConcurrencyAware, IMapToEvent<PhotoAdded>
    {
        public AddPhotoCommand(Guid aggregateRootId, int concurrencyId, string photo) 
        {
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Photo = photo;
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

        public PhotoAdded GetEvent()
        {
            return new PhotoAdded(this.Id, this.ConcurrencyId + 1, this.Photo);
        }
    }
}
