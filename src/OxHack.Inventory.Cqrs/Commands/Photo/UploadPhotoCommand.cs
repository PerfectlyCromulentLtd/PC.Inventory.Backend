using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Photo
{
    public class UploadPhotoCommand : ICommand
    {
        public UploadPhotoCommand(byte[] photoData, string folder) 
        {
            this.PhotoData = photoData;
            this.Folder = folder;
        }

		public Guid Id
			=> Guid.Empty;

		public byte[] PhotoData
        {
            get;
        }

        public string Folder
        {
            get;
        }

        public string ResultingFileName
        {
            get;
            set;
        }
	}
}
