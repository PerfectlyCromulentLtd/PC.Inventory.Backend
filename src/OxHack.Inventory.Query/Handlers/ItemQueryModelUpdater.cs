using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Query.Models;
using OxHack.Inventory.Query.Repositories;
using System;
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
		IHandle<PhotoRemoved>,
        IHandle<ItemUpdated>
	{
		private static string PlaceholderImage
			=> "placeholder.jpg";

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
				var model = new MutableItem();

				message.Apply(model);

				await this.itemRepository.CreateItemAsync(model);
				if (!model.Photos?.Any() ?? true)
				{
					await this.photoRepository.AddPhotoToItemAsync(message.Id, ItemQueryModelUpdater.PlaceholderImage);
				}
			}
			catch
			{
				// TODO: Log that event replay is needed
				throw;
			}
		}

		public async Task Handle(AdditionalInformationChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(AppearanceChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(AssignedLocationChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(CategoryChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(CurrentLocationChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(IsLoanChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(ManufacturerChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(ModelChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(NameChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(OriginChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(QuantityChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(SpecChanged message) =>
			await this.PersistEventChanges(message, item => message.Apply(item));

		public async Task Handle(PhotoAdded message) =>
			await this.PersistEventChanges(message, item =>
			{
				message.Apply(item);

				this.photoRepository.AddPhotoToItemAsync(item.Id, message.PhotoFilename);

				if (item.Photos.Contains(ItemQueryModelUpdater.PlaceholderImage))
				{
					this.photoRepository.RemovePhotoFromItemAsync(item.Id, ItemQueryModelUpdater.PlaceholderImage);

					var photos = item.Photos.ToList();
					photos.RemoveAll(photo => photo == ItemQueryModelUpdater.PlaceholderImage);
					item.Photos = photos;
				}
			});

		public async Task Handle(PhotoRemoved message) =>
			await this.PersistEventChanges(message, item =>
			{
				message.Apply(item);

				this.photoRepository.RemovePhotoFromItemAsync(item.Id, message.PhotoFilename);

				if (!item.Photos.Any())
				{
					this.photoRepository.AddPhotoToItemAsync(item.Id, ItemQueryModelUpdater.PlaceholderImage);

					var photos = item.Photos.ToList();
					photos.Add(ItemQueryModelUpdater.PlaceholderImage);
					item.Photos = photos;
				}
			});

        public async Task Handle(ItemUpdated message) =>
            await this.PersistEventChanges(message, item => message.Apply(item));

        private async Task PersistEventChanges<TEvent>(TEvent @event, Action<Item> mutation) where TEvent : IAggregateEvent
		{
			try
			{
				var item = await this.itemRepository.GetItemByIdAsync(@event.Id);

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
