using Microsoft.Data.Entity;
using OxHack.Inventory.Query.Models;
using OxHack.Inventory.Query.Repositories;
using OxHack.Inventory.Query.Sqlite.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Sqlite.Repositories
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
			var items = await 
				this.dbContext.Items
					.Include(item => item.Photos)
					.IncludeAllMembers()
					.ToListAsync();

			var immutables = items.Select(item => item.ToImmutableModel());

			return immutables;
		}

		public async Task<Item> GetByIdAsync(Guid id)
		{
			var result = await
				this.dbContext.Items
					.IncludeAllMembers()
					.SingleOrDefaultAsync(item => item.Id == id.ToString());

			return result?.ToImmutableModel();					
		}
	}
}
