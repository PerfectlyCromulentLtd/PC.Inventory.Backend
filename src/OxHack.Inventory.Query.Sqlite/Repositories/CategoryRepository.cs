using Microsoft.EntityFrameworkCore;
using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Sqlite.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DbContextOptions dbContextOptions;

		public CategoryRepository(DbContextOptions dbContextOptions)
		{
			this.dbContextOptions = dbContextOptions;
		}

		public async Task<IEnumerable<string>> GetAllCategoriesAsync()
		{
			using (var dbContext = new InventoryDbContext(this.dbContextOptions))
			{
				var query =
					dbContext.Items
						.Select(item => item.Category)
						.Distinct()
						.OrderBy(item => item.ToLower());

				var results = await query.ToListAsync();

				results = results.Where(category => !String.IsNullOrWhiteSpace(category)).ToList();

				return results;
			}
		}
	}
}
