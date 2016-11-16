using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using System.Threading.Tasks;
using System;
using OxHack.Inventory.Query.Repositories;
using OxHack.Inventory.Cqrs.Commands.Photo;

namespace OxHack.Inventory.Command.Handlers
{
    internal class PhotoCommandHandler :
		IHandle<UploadAndAddPhotoCommand>,
		IHandle<UploadPhotoCommand>,
		IHandle<AddPhotoCommand>,
		IHandle<RemovePhotoCommand>
    {
        private readonly IEventStore eventStore;
        private readonly IPhotoRepository photoRepo;

        public PhotoCommandHandler(IEventStore eventStore, IPhotoRepository photoRepo)
        {
            this.eventStore = eventStore;
            this.photoRepo = photoRepo;
		}

		public async Task Handle(UploadAndAddPhotoCommand message)
		{
			await this.Handle(message.UploadPhotoCommand);
			message.AddPhotoCommand.FileName = message.UploadPhotoCommand.ResultingFileName;

			await this.Handle(message.AddPhotoCommand);
		}

		public async Task Handle(UploadPhotoCommand message)
		{
			message.ResultingFileName = await this.photoRepo.StorePhotoAsync(message.PhotoData, message.Folder);
			var @event = message.GetEvent();
			this.eventStore.StoreEvent("Photo Uploads", @event);
		}

		public async Task Handle(AddPhotoCommand message)
        {
            var @event = message.GetEvent();
            this.eventStore.StoreAggregateEvent(@event);
            await Task.WhenAll();
		}

		public async Task Handle(RemovePhotoCommand message)
        {
            var @event = message.GetEvent();
            this.eventStore.StoreAggregateEvent(@event);
            await Task.WhenAll();
        }
    }
}
