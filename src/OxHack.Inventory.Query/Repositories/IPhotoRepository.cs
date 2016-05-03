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
		Task AddPhotoToItem(Guid itemId, string photoFilename);
		Task RemovePhotoFromItem(Guid itemId, string photoFilename);
	}
}
