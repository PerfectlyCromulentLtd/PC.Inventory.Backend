using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using System.Threading.Tasks;
using System;
using OxHack.Inventory.Query.Repositories;

namespace OxHack.Inventory.Command.Handlers
{
    internal class PhotoCommandHandler :
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

        public async Task Handle(AddPhotoCommand message)
        {
            message.FileName = await this.photoRepo.StorePhotoAsync(message.PhotoData, message.Folder);

            var @event = message.GetEvent();
            this.eventStore.StoreEvent(@event);
            await Task.WhenAll();
        }

        public async Task Handle(RemovePhotoCommand message)
        {
            var @event = message.GetEvent();
            this.eventStore.StoreEvent(@event);
            await Task.WhenAll();
        }
    }
}
