using OxHack.Inventory.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Models
{
	public class Category : IConcurrencyAware
	{
		public int ConcurrencyId
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
