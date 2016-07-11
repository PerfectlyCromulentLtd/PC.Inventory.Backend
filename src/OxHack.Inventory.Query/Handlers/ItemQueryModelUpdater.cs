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
		private static string PlaceholderImage => "placeholder.jpg";

		private readonly IItemRepository itemRepository;
		private readonly IPhotoRepository photoRepository;

		public ItemQueryModelUpdater(IItemRepository itemRepository, IPhotoRepository photoRepository)
		{
			this.itemRepository = itemRepository;
			this.photoRepository = photoRepository;
		}

		public async Task Handle(ItemCreated message)
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
				await this.photoRepository.AddPhotoToItemAsync(message.AggregateRootId, ItemQueryModelUpdater.PlaceholderImage);
			}
			catch
			{
				// TODO: Log that event replay is needed
				throw;
			}
		}

		public async Task Handle(AdditionalInformationChanged message) =>
			await this.SaveMutation(message, item => item.AdditionalInformation = message.AdditionalInformation);

		public async Task Handle(AppearanceChanged message) =>
			await this.SaveMutation(message, item => item.Appearance = message.Appearance);

		public async Task Handle(AssignedLocationChanged message) =>
			await this.SaveMutation(message, item => item.AssignedLocation = message.AssignedLocation);

		public async Task Handle(CategoryChanged message) =>
			await this.SaveMutation(message, item => item.Category = message.Category);

		public async Task Handle(CurrentLocationChanged message) =>
			await this.SaveMutation(message, item => item.CurrentLocation = message.CurrentLocation);

		public async Task Handle(IsLoanChanged message) =>
			await this.SaveMutation(message, item => item.IsLoan = message.IsLoan);

		public async Task Handle(ManufacturerChanged message) =>
			await this.SaveMutation(message, item => item.Manufacturer = message.Manufacturer);

		public async Task Handle(ModelChanged message) =>
			await this.SaveMutation(message, item => item.Model = message.Model);

		public async Task Handle(NameChanged message) =>
			await this.SaveMutation(message, item => item.Name = message.Name);

		public async Task Handle(OriginChanged message) =>
			await this.SaveMutation(message, item => item.Origin = message.Origin);

		public async Task Handle(QuantityChanged message) =>
			await this.SaveMutation(message, item => item.Quantity = message.Quantity);

		public async Task Handle(SpecChanged message) =>
			await this.SaveMutation(message, item => item.Spec = message.Spec);

		public async Task Handle(PhotoAdded message) =>
			await this.SaveMutation(message, item =>
			{
				var photos = item.Photos.ToList();
				item.Photos = photos;

				photos.Add(message.PhotoFilename);
				photos.RemoveAll(photo => photo == ItemQueryModelUpdater.PlaceholderImage);
			});

		public async Task Handle(PhotoRemoved message) =>
			await this.SaveMutation(message, item =>
			{
				var photos = item.Photos.ToList();
				item.Photos = photos;

				photos.Remove(message.PhotoFilename);

				if (photos.Count == 0)
				{
					photos.Add(ItemQueryModelUpdater.PlaceholderImage);
				}
			});

		private async Task SaveMutation<TEvent>(ConcurrencyAwareEvent<TEvent> @event, Action<Item> mutation) where TEvent : IEvent
		{
			try
			{
				var item = await this.itemRepository.GetItemByIdAsync(@event.AggregateRootId);

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
