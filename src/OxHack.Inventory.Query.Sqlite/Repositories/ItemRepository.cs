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
        private readonly SqliteWriteLock writeLock;

        public ItemRepository(DbContextOptions dbContextOptions, SqliteWriteLock writeLock)
        {
            this.dbContextOptions = dbContextOptions;
            this.writeLock = writeLock;
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

        public async Task<Item> GetItemByIdAsync(Guid id)
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

		public async Task<IEnumerable<Item>> GetItemsByCategoryAsync(string category)
		{
			using (var dbContext = new InventoryDbContext(this.dbContextOptions))
			{
				var items = await
					dbContext.Items
						.AsNoTracking()
						.IncludeAllMembers()
						.Where(item => item.Category.Trim() == category.Trim())
						.ToListAsync();

				var immutables = items.Select(item => item.ToImmutableModel());

				return immutables;
			}
		}

		public async Task CreateItemAsync(Item item)
        {
            var dbModel = item.ToDbModel();

            // Hack until EF7 supports offline concurrency token generation
            // See https://github.com/aspnet/EntityFramework/issues/2195
            lock (this.writeLock)
            {
                dbModel.ConcurrencyId = Guid.NewGuid().ToString();

                using (var dbContext = new InventoryDbContext(this.dbContextOptions))
                {
                    dbContext.Items.Add(dbModel);
                    dbContext.SaveChanges();
                }
            }
        }

        public async Task UpdateItemAsync(Item item)
        {
            var dbModel = item.ToDbModel();

            // Hack until EF7 supports offline concurrency token generation
            // See https://github.com/aspnet/EntityFramework/issues/2195
            lock (this.writeLock)
            {
                using (var dbContext = new InventoryDbContext(this.dbContextOptions))
                {
					var concurrencyId = dbContext.Items.AsNoTracking().Where(record => record.Id == dbModel.Id).Select(record => record.ConcurrencyId).SingleOrDefault();

                    if (concurrencyId != dbModel.ConcurrencyId)
                    {
                        throw new OptimisticConcurrencyException($"Error updating Item {dbModel.Id}.  It seems someone beat you to the punch!");
                    }
                    dbModel.ConcurrencyId = Guid.NewGuid().ToString();

                    dbContext.Items.Attach(dbModel, GraphBehavior.SingleObject);
                    dbContext.Entry(dbModel).State = EntityState.Modified;

                    dbContext.SaveChanges();
                }
            }
        }
	}
}
