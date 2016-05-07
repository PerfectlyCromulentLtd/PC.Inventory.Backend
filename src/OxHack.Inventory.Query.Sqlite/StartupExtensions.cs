using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OxHack.Inventory.Query.Repositories;
using OxHack.Inventory.Query.Sqlite.Repositories;
using System;

namespace OxHack.Inventory.Query.Sqlite
{
	public static class StartupExtensions
    {
        public static void RegisterRepositories(this IServiceCollection @this, IConfigurationRoot configuration)
        {
            @this
                .AddEntityFramework()
                .AddSqlite();

			var connectionString = configuration["Production:SqliteReadModelConnectionString"];
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite(connectionString);

            @this.AddSingleton<DbContextOptions>(sp => optionsBuilder.Options);

            SqliteWriteLock syncLock = new SqliteWriteLock();
            @this.AddSingleton<SqliteWriteLock>(sp => syncLock);

            @this.AddTransient<IItemRepository, ItemRepository>();
			@this.AddTransient<IPhotoRepository, PhotoRepository>();
			@this.AddTransient<ICategoryRepository, CategoryRepository>();

			var provider = @this.BuildServiceProvider();
			var environment = provider.GetRequiredService<IHostingEnvironment>();

			if (environment.IsDevelopment())
			{
				var dbContext = new InventoryDbContext(optionsBuilder.Options);
				var contextServices = ((IInfrastructure<IServiceProvider>)dbContext).Instance;
				var loggerFactory = contextServices.GetRequiredService<ILoggerFactory>();
				loggerFactory.AddConsole(LogLevel.Information);
			}
		}
    }
}
