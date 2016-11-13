using Microsoft.EntityFrameworkCore;
using OxHack.Inventory.Query.Repositories;
using System;
using System.IO;
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
			using (var dbContext = new InventoryDbContext(this.dbContextOptions))
			{
				var newPhoto = new Models.Photo()
				{
					Filename = photoFilename,
					ItemId = itemId.ToString()
				};

				dbContext.Photos.Add(newPhoto);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task RemovePhotoFromItemAsync(Guid itemId, string photoFilename)
		{
			using (var dbContext = new InventoryDbContext(this.dbContextOptions))
			{
				var itemIdAsString = itemId.ToString();
				var match = 
					dbContext
						.Photos
						.AsNoTracking()
						.SingleOrDefault(photo => photo.ItemId == itemIdAsString && photo.Filename == photoFilename);

				if (match != null)
				{
					dbContext.Photos.Remove(match);
					await dbContext.SaveChangesAsync();
				}
			}
		}

		public async Task<string> StorePhotoAsync(byte[] photoData, string folder)
		{
			FileInfo photoFile;
			do
			{
				photoFile = new FileInfo(
					Path.Combine(folder, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()))
					+ ".jpg");

			}
			while (photoFile.Exists);

			using (var stream = photoFile.Create())
			{
				await stream.WriteAsync(photoData, 0, photoData.Length);
			}

			return photoFile.Name;
		}
	}
}
