using Microsoft.Data.Entity;
using OxHack.Inventory.Data.Models;
using OxHack.Inventory.Data.Repositories;
using OxHack.Inventory.Data.Sqlite.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Data.Sqlite.Repositories
{
	public class ItemRepository : IItemRepository
	{
		private readonly InventoryDbContext dbContext;

		public ItemRepository(InventoryDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<Item>> GetAllItemsAsync()
		{
			var items = await this.dbContext.Items.Include(item => item.Photos).ToListAsync();

			var immutables = items.Select(item => item.ToImmutableModel());

			return immutables;
		}

		public async Task<Item> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
