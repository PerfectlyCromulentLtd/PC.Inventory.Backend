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

        public async Task Handle(CreateItemCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeAdditionalInformationCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeAppearanceCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeAssignedLocationCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeCategoryCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeCurrentLocationCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeIsLoanCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeManufacturerCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeModelCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeNameCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeOriginCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeQuantityCommand message)
        {
            await this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeSpecCommand message)
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
