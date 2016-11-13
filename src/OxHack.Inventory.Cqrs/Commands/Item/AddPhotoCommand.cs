using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class AddPhotoCommand : ICommand, IConcurrencyAware, IMapToEvent<PhotoAdded>
    {
        public AddPhotoCommand(Guid aggregateRootId, int concurrencyId, byte[] photoData, string folder) 
        {
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.PhotoData = photoData;
            this.Folder = folder;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public byte[] PhotoData
        {
            get;
        }

        public string Folder
        {
            get;
        }

        public string FileName
        {
            get;
            set;
        }

        public PhotoAdded GetEvent()
        {
            return new PhotoAdded(this.Id, this.ConcurrencyId + 1, this.FileName);
        }
    }
}
