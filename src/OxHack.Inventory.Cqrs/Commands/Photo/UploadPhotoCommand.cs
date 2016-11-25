using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Cqrs.Events.Photo;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Photo
{
	public class UploadPhotoCommand : ICommand, IMapToEvent<PhotoUploaded>
	{
		public UploadPhotoCommand(byte[] photoData, string folder, dynamic issuerMetadata)
		{
			this.PhotoData = photoData;
			this.Folder = folder;
			this.IssuerMetadata = issuerMetadata;
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

		public dynamic IssuerMetadata
		{
			get;
		}

		public PhotoUploaded GetEvent()
		{
			return new PhotoUploaded(this.ResultingFileName);
		}
	}
}
