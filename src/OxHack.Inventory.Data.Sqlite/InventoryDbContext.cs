using Microsoft.Data.Entity;
using OxHack.Inventory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Data.Sqlite
{
    public class InventoryDbContext : DbContext
    {
		public DbSet<Item> Items
		{
			get; set;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Item>().HasKey(m => m.Id);
			base.OnModelCreating(builder);
		}
	}
}
