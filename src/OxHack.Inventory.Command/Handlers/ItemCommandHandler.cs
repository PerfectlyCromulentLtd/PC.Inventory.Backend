using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Handlers
{
    internal class ItemCommandHandler :
        IHandle<CreateItemCommand>,
        IHandle<ChangeAdditionalInformationCommand>,
        IHandle<ChangeAppearanceCommand>,
        IHandle<ChangeAssignedLocationCommand>,
        IHandle<ChangeCategoryCommand>,
        IHandle<ChangeCurrentLocationCommand>,
        IHandle<ChangeIsLoanCommand>,
        IHandle<ChangeManufacturerCommand>,
        IHandle<ChangeModelCommand>,
        IHandle<ChangeNameCommand>,
        IHandle<ChangeOriginCommand>,
        IHandle<ChangeQuantityCommand>,
        IHandle<ChangeSpecCommand>
    {
        private readonly IBus bus;

        public ItemCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async void Handle(CreateItemCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeAdditionalInformationCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeAppearanceCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeAssignedLocationCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeCategoryCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeCurrentLocationCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeIsLoanCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeManufacturerCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeModelCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeNameCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeOriginCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeQuantityCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async void Handle(ChangeSpecCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        private async Task PublishMappedEvent<TEvent>(IMapToEvent<TEvent> source) where TEvent : IEvent
        {
            var @event = source.GetEvent();

            await this.bus.RaiseEventAsync(@event);
        }
    }
}
