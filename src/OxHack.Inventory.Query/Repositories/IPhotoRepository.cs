using OxHack.Inventory.Query.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Repositories
{
	public interface IPhotoRepository
	{
		Task<string> StorePhotoAsync(byte[] photoData, string folder);
		Task AddPhotoToItemAsync(Guid itemId, string photoFilename);
		Task RemovePhotoFromItemAsync(Guid itemId, string photoFilename);
	}
}
