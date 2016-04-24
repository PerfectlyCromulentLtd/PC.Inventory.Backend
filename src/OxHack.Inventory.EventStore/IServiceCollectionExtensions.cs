using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using NEventStore.Persistence.Sql;
using NEventStore.Persistence.Sql.SqlDialects;
using System.Configuration;
using System.Data;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events.Item;

namespace OxHack.Inventory.EventStore
{
	public static class IServiceCollectionExtensions
	{
		public static void AddEventStore(this IServiceCollection @this, IConfigurationRoot configuration)
		{
			// begin hack
			var dataSet = ConfigurationManager.GetSection("system.data") as DataSet;
			dataSet.Tables[0].Rows.Add(
				"SQLite Data Provider",
				".Net Framework Data Provider for SQLite",
				"System.Data.SQLite",
				"System.Data.SQLite.SQLiteFactory, System.Data.SQLite");
			// end hack
			var configFactory = new AspNetConfigurationConnectionFactory("Production:SqliteEventStoreConnectionString", "System.Data.SQLite", configuration);

			var meh = configFactory.GetDbProviderFactoryType();

			var store = 
				Wireup.Init()
					.UsingSqlPersistence(configFactory)
						.WithDialect(new SqliteDialect())
						.InitializeStorageEngine()
						.UsingJsonSerialization()
					.Build();

            var eventStore = new NEventStoreEventStore(store);

            @this.AddSingleton<IEventStore, NEventStoreEventStore>(s => eventStore);
		}
    }
}
