using OxHack.Inventory.Cqrs.Commands.Photo;
using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class UploadAndAddPhotoCommand : ICommand, IConcurrencyAware, IMapToEvent<PhotoAdded>
    {
		public UploadAndAddPhotoCommand(Guid aggregateRootId, int concurrencyId, byte[] photoData, string folder, dynamic issuerMetadata)
		{
			this.AddPhotoCommand = new AddPhotoCommand(aggregateRootId, concurrencyId, issuerMetadata);
			this.UploadPhotoCommand = new UploadPhotoCommand(photoData, folder, issuerMetadata);
			this.IssuerMetadata = issuerMetadata;
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

		public dynamic IssuerMetadata
		{
			get;
		}

		public PhotoAdded GetEvent()
			=> this.AddPhotoCommand.GetEvent();
    }
}
