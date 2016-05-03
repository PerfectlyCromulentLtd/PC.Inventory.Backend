using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Query.Models;
using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Handlers
{
    public class ItemQueryModelUpdater :
        IHandle<ItemCreated>,
        IHandle<AdditionalInformationChanged>,
        IHandle<AppearanceChanged>,
        IHandle<AssignedLocationChanged>,
        IHandle<CategoryChanged>,
        IHandle<CurrentLocationChanged>,
        IHandle<IsLoanChanged>,
        IHandle<ManufacturerChanged>,
        IHandle<ModelChanged>,
        IHandle<NameChanged>,
        IHandle<OriginChanged>,
        IHandle<QuantityChanged>,
        IHandle<SpecChanged>,
		IHandle<PhotoAdded>,
		IHandle<PhotoRemoved>
	{
        private readonly IItemRepository itemRepository;
		private readonly IPhotoRepository photoRepository;

		public ItemQueryModelUpdater(IItemRepository itemRepository, IPhotoRepository photoRepository)
        {
            this.itemRepository = itemRepository;
			this.photoRepository = photoRepository;
		}

        public async void Handle(ItemCreated message)
        {
            try
            {
                var model =
                    new Item(
                        message.AggregateRootId,
                        message.AdditionalInformation,
                        message.Appearance,
                        message.AssignedLocation,
                        message.Category,
                        message.CurrentLocation,
                        message.IsLoan,
                        message.Manufacturer,
                        message.Model,
                        message.Name,
                        message.Origin,
                        message.Quantity,
                        message.Spec,
                        null,
                        Guid.Empty);

                await this.itemRepository.CreateItemAsync(model);
            }
            catch
            {
                // TODO: Log that event replay is needed
                throw;
            }
        }

        public async void Handle(AdditionalInformationChanged message) =>
            await this.SaveMutation(message, item => item.AdditionalInformation = message.AdditionalInformation);

        public async void Handle(AppearanceChanged message) =>
            await this.SaveMutation(message, item => item.Appearance = message.Appearance);

        public async void Handle(AssignedLocationChanged message) =>
            await this.SaveMutation(message, item => item.AssignedLocation = message.AssignedLocation);

        public async void Handle(CategoryChanged message) =>
            await this.SaveMutation(message, item => item.Category = message.Category);

        public async void Handle(CurrentLocationChanged message) =>
            await this.SaveMutation(message, item => item.CurrentLocation = message.CurrentLocation);

        public async void Handle(IsLoanChanged message) =>
            await this.SaveMutation(message, item => item.IsLoan = message.IsLoan);

        public async void Handle(ManufacturerChanged message) =>
            await this.SaveMutation(message, item => item.Manufacturer = message.Manufacturer);

        public async void Handle(ModelChanged message) =>
            await this.SaveMutation(message, item => item.Model = message.Model);

        public async void Handle(NameChanged message) =>
            await this.SaveMutation(message, item => item.Name = message.Name);

        public async void Handle(OriginChanged message) =>
            await this.SaveMutation(message, item => item.Origin = message.Origin);

        public async void Handle(QuantityChanged message) =>
            await this.SaveMutation(message, item => item.Quantity = message.Quantity);

        public async void Handle(SpecChanged message) =>
            await this.SaveMutation(message, item => item.Spec = message.Spec);

		public async void Handle(PhotoAdded message)
		{
			await this.photoRepository.AddPhotoToItem(message.AggregateRootId, message.PhotoFilename);
		}

		public async void Handle(PhotoRemoved message)
		{
			await this.photoRepository.RemovePhotoFromItem(message.AggregateRootId, message.PhotoFilename);
		}

		private async Task SaveMutation<TEvent>(TEvent @event, Action<Item> mutation) where TEvent : IEvent, IConcurrencyAware
        {
            try
            {
                var item = await this.itemRepository.GetByIdAsync(@event.AggregateRootId);

                mutation(item);
                item.ConcurrencyId = @event.ConcurrencyId;

                await this.itemRepository.UpdateItemAsync(item);
            }
            catch
            {
                // TODO: Log that event replay is needed
                throw;
            }
        }
    }
}
