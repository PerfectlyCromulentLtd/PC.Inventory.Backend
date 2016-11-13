using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Controllers
{
	[Obsolete("This is a debugging tool.  It should NOT go out to production.")]
	[Route("api/v1/[controller]")]
	public class MigrationController : Controller
	{
		private readonly ItemService itemService;
		private readonly IEventStore eventStore;
		private readonly IBus bus;

		public MigrationController(IHostingEnvironment environment, ItemService itemService, IEventStore eventStore, IBus bus)
		{
			if (environment.IsProduction())
			{
				throw new InvalidOperationException("This controller cannot be used in Production.");
			}

			this.itemService = itemService;
			this.eventStore = eventStore;
			this.bus = bus;
		}

		[HttpGet]
		[Obsolete("Untested")]
		public async Task<dynamic> DoStuff(
			[FromQuery] bool generateEventsFromQueryDb = false,
			[FromQuery] bool generateQueryDbFromEvents = false,
			[FromQuery] bool persistResults = false)
		{
			dynamic result = "Nothing";

			if (generateEventsFromQueryDb && generateQueryDbFromEvents)
			{
				return this.BadRequest();
			}

			if (generateEventsFromQueryDb)
			{
				var events = await GenerateEventsFromQueryDb(persistResults);

				result = events.Select(entry => new
				{
					Type = entry.GetType().Name,
					Body = entry
				});
			}

			if (generateQueryDbFromEvents)
			{
				var eventsByItemId =
					this.eventStore
						.GetAllEvents()
						.OrderBy(item => item.CommitStamp)
						.Select(item => item.Event)
						.ToList();

				foreach (var @event in eventsByItemId)
				{
					await this.bus.RaiseEventAsync(@event);
				}

				result = $"{eventsByItemId.Count} events replayed.";
			}

			return result;
		}

		private async Task<IEnumerable<IEvent>> GenerateEventsFromQueryDb(bool persistResults)
		{
			var items = (await this.itemService.GetAllItemsAsync()).ToList();

			var itemCreationEvents =
				items
					.Select(item =>
						new ItemCreated(
							item.Id,
							item.AdditionalInformation,
							item.Appearance,
							item.AssignedLocation,
							item.Category,
							item.CurrentLocation,
							item.IsLoan,
							item.Manufacturer,
							item.Model,
							item.Name,
							item.Origin,
							item.Quantity,
							item.Spec))
					.ToList();

			List<PhotoAdded> photoAddedEvents =
				items
					.SelectMany(item =>
						item.Photos
							.Where(photo => photo != "placeholder.jpg")
							.Select((photo, index) => new PhotoAdded(item.Id, index + 2, photo)))
					.ToList();

			var allEvents =
				itemCreationEvents
					.Cast<IEvent>()
					.Concat(photoAddedEvents);

			if (persistResults)
			{
				foreach (var @event in allEvents)
				{
					this.eventStore.StoreEvent(@event);
				}
			}

			return allEvents;
		}
	}
}