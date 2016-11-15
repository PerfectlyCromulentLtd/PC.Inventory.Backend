using OxHack.Inventory.Cqrs.Commands.Photo;
using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class UploadAndAddPhotoCommand : ICommand, IConcurrencyAware, IMapToEvent<PhotoAdded>
    {
		public UploadAndAddPhotoCommand(Guid aggregateRootId, int concurrencyId, byte[] photoData, string folder) 
        {
			this.AddPhotoCommand = new AddPhotoCommand(aggregateRootId, concurrencyId);
			this.UploadPhotoCommand = new UploadPhotoCommand(photoData, folder);
        }

		public AddPhotoCommand AddPhotoCommand
		{
			get;
		}

		public UploadPhotoCommand UploadPhotoCommand
		{
			get;
		}

		public Guid Id
			=> this.AddPhotoCommand.Id;

		public int ConcurrencyId
			=> this.AddPhotoCommand.ConcurrencyId;

		public string ResultingFileName
			=> this.UploadPhotoCommand.ResultingFileName;

		public PhotoAdded GetEvent()
			=> this.AddPhotoCommand.GetEvent();
    }
}
