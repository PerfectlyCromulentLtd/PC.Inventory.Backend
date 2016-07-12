using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OxHack.Inventory.Query.Sqlite.Models;
using System.Collections.Generic;
using System.Linq;

namespace OxHack.Inventory.Query.Sqlite.Extensions
{
	public static class QueryExtensions
	{
		internal static IIncludableQueryable<T, IEnumerable<Photo>> IncludeAllMembers<T>(this IQueryable<T> source) where T : Item
		{
			return source.Include(item => item.Photos);
		}
	}
}
