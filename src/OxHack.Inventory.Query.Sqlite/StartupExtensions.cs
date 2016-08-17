using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
        public static void RegisterRepositories(this IServiceCollection @this, IConfigurationRoot configuration, IHostingEnvironment hostingEnvironment)
        {
			@this.AddEntityFramework();

			var connectionString = configuration[hostingEnvironment.EnvironmentName + ":SqliteReadModelConnectionString"];
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite(connectionString);

            @this.AddSingleton<DbContextOptions>(sp => optionsBuilder.Options);

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
