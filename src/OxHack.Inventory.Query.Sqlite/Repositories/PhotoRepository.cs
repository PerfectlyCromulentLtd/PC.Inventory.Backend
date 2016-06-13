using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Sqlite.Repositories
{
	public class PhotoRepository : IPhotoRepository
	{
		private readonly DbContextOptions dbContextOptions;

		public PhotoRepository(DbContextOptions dbContextOptions)
		{
			this.dbContextOptions = dbContextOptions;
		}

		public async Task AddPhotoToItemAsync(Guid itemId, string photoFilename)
		{
			try
			{
				using (var dbContext = new InventoryDbContext(this.dbContextOptions))
				{
					var newPhoto = new Models.Photo()
					{
						Filename = photoFilename,
						ItemId = itemId.ToString()
					};

					dbContext.Photos.Add(newPhoto, GraphBehavior.SingleObject);
					await dbContext.SaveChangesAsync();
				}
			}
			catch (Exception e)
			{
				throw;
			}
		}

		public async Task RemovePhotoFromItemAsync(Guid itemId, string photoFilename)
		{
			using (var dbContext = new InventoryDbContext(this.dbContextOptions))
			{
				var itemIdAsString = itemId.ToString();
				var match = dbContext.Photos.SingleOrDefault(photo => photo.ItemId == itemIdAsString && photo.Filename == photoFilename);

				if (match != null)
				{
					dbContext.Photos.Remove(match);
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
