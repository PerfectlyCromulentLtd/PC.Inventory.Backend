using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using NEventStore.Persistence.Sql;
using NEventStore.Persistence.Sql.SqlDialects;
using System.Configuration;
using System.Data;

namespace OxHack.Inventory.EventStore
{
	public static class Startup
	{
		public static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
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

			//using (store)
			//{
			//	// some business code here
			//	using (var stream = store.CreateStream(myMessage.CustomerId))
			//	{
			//		stream.Add(new EventMessage { Body = myMessage });
			//		stream.CommitChanges(myMessage.MessageId);
			//	}

			//	using (var stream = store.OpenStream(myMessage.CustomerId, 0, int.MaxValue))
			//	{
			//		foreach (var @event in stream.CommittedEvents)
			//		{
			//			// business processing...
			//		}
			//	}
			//}
			services.AddSingleton<IStoreEvents>(s => store);
		}
	}
}
