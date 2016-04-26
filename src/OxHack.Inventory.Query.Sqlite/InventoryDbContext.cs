using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using OxHack.Inventory.Query.Sqlite.Models;

namespace OxHack.Inventory.Query.Sqlite
{
    internal class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Item>(entity =>
			{
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.ConcurrencyId);
                // disabled until EF7 supports offline concurrency token generation
                // See https://github.com/aspnet/EntityFramework/issues/2195
                //.ValueGeneratedOnAddOrUpdate()
                //.IsConcurrencyToken();
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
