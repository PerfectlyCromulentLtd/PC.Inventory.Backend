using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
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
        private readonly DbContextOptions dbContextOptions;

        public ItemRepository(DbContextOptions dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
		{
            using (var dbContext = new InventoryDbContext(this.dbContextOptions))
            {
                var items = await
                    dbContext.Items
                        .AsNoTracking()
                        .Include(item => item.Photos)
                        .IncludeAllMembers()
                        .ToListAsync();

                var immutables = items.Select(item => item.ToImmutableModel());

                return immutables;
            }
		}

		public async Task<Item> GetByIdAsync(Guid id)
		{
            using (var dbContext = new InventoryDbContext(this.dbContextOptions))
            {
                var result = await
                dbContext.Items
                    .AsNoTracking()
                    .IncludeAllMembers()
                    .SingleOrDefaultAsync(item => item.Id == id.ToString());

                return result?.ToImmutableModel();
            }
        }

        public async Task CreateItemAsync(Item item)
        {
            var dbModel = item.ToDbModel();

            using (var dbContext = new InventoryDbContext(this.dbContextOptions))
            {
                dbContext.Items.Add(dbModel);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateItemAsync(Item item)
        {
            var dbModel = item.ToDbModel();

            using (var dbContext = new InventoryDbContext(this.dbContextOptions))
            {
                dbContext.Items.Attach(dbModel, GraphBehavior.SingleObject);
                dbContext.Entry(dbModel).State = EntityState.Modified;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
