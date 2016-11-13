using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxHack.Inventory.Query.Models;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands;

namespace OxHack.Inventory.Services
{
	public class ItemService
	{
		private readonly IItemRepository itemRepo;
		private readonly IPhotoRepository photoRepo;
		private readonly IBus bus;

		public ItemService(IItemRepository itemRepo, IPhotoRepository photoRepo, IBus bus)
		{
			this.itemRepo = itemRepo;
			this.photoRepo = photoRepo;
			this.bus = bus;
		}

		public async Task<IEnumerable<Item>> GetAllItemsAsync()
		{
			return await this.itemRepo.GetAllItemsAsync();
		}

		public async Task<Item> GetItemByIdAsync(Guid id)
		{
			return await this.itemRepo.GetItemByIdAsync(id);
		}

		public async Task IssueCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
		{
			await bus.IssueCommandAsync(command);
		}

		public async Task<IEnumerable<Item>> GetItemsByCategoryAsync(string category)
		{
			return await this.itemRepo.GetItemsByCategoryAsync(category);
		}

		public async Task<string> AddPhotoToItemAsync(Guid itemId, byte[] photoData, string folder)
		{
			var filename = await this.photoRepo.StorePhotoAsync(photoData, folder);
			await this.photoRepo.AddPhotoToItemAsync(itemId, filename);

			return filename;
		}
	}
}
