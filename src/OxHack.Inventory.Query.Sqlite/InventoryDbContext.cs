using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using OxHack.Inventory.Query.Sqlite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Sqlite
{
    public class InventoryDbContext : DbContext
    {
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Item>(entity =>
			{
				entity.Property(e => e.Appearance).IsRequired();

				entity.Property(e => e.AssignedLocation).IsRequired();

				entity.Property(e => e.Category).IsRequired();

				entity.Property(e => e.Name).IsRequired();
			});

			modelBuilder.Entity<Photo>(entity =>
			{
				entity.HasKey(e => e.Filename);

				entity.HasOne(d => d.Item).WithMany(p => p.Photos).HasForeignKey(d => d.ItemId).OnDelete(DeleteBehavior.Restrict);
			});
		}

		internal virtual DbSet<Item> Items
		{
			get; set;
		}

		internal virtual DbSet<Photo> Photos
		{
			get; set;
		}
	}
}
