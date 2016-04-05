using Microsoft.Data.Entity;
using OxHack.Inventory.Data.Models;
using OxHack.Inventory.Data.Repositories;
using System;
using System.Collections.Generic;
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
			return await this.dbContext.Items.ToListAsync();
		}

		public async Task<Item> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
