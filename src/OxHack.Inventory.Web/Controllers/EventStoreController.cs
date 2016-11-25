using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Controllers
{
	[Obsolete("This is a debugging tool.  It should NOT go out to production.")]
	[Route("api/v1/[controller]")]
	public class EventStoreController : Controller
	{
		private readonly IEventStore eventStore;
		private readonly IBus bus;
		private readonly ItemService itemService;

		public EventStoreController(
			IHostingEnvironment environment, 
			IEventStore eventStore, 
			IBus bus, 
			ItemService itemService)
		{
			if (environment.IsProduction())
			{
				throw new InvalidOperationException("This controller cannot be used in Production.");
			}

			this.eventStore = eventStore;
			this.bus = bus;
			this.itemService = itemService;
		}

		[HttpGet("{aggregateId}")]
		public async Task<dynamic> GetByAggregateId(Guid aggregateId, [FromQuery] bool replay = false)
		{
			var events = this.eventStore.GetEventsByAggregateId(aggregateId);

			if (replay)
			{
				await this.ReplayEvents(events);
			}

			return events;
		}

		[HttpGet]
		public async Task<dynamic> GetAll(
			[FromQuery] bool replayToBus = false, 
			[FromQuery] bool replayToMemory = false,
			[FromQuery] bool buildEventsFromData = false)
		{
			var replay = replayToBus || replayToMemory;

			if (replay && buildEventsFromData)
			{
				return this.BadRequest($"{nameof(replay)} and {nameof(buildEventsFromData)} flags are mutually exclusive.");
			}

			if (buildEventsFromData)
			{
				await this.BuildEventsFromData();
			}

			var events = this.eventStore.GetAllEvents();

			dynamic result =
				events.Select(item => new
				{
					Type = item.Event.GetType().Name,
					EventInfo = item
				});

			if (replayToBus)
			{
				await this.ReplayEvents(events);
			}

			if (replayToMemory)
			{
				var aggregates =
					events
						.Select(item => item.Event)
						.OfType<IAggregateEvent>()
						.GroupBy(item => item.Id)
						.Select(stream => stream.OrderBy(item => item.ConcurrencyId).Aggregate(new ExpandoObject(), (aggregate, @event) => @event.Apply(aggregate)));

				result = aggregates;
			}

			return result;
		}

		private async Task ReplayEvents(IEnumerable<StoredEvent> events)
		{
			var eventsToReplay = events.OrderBy(item => item.CommitStamp).Select(item => item.Event).ToList();

			foreach (var @event in eventsToReplay)
			{
				await this.bus.RaiseEventAsync(@event);
			}
		}

		private async Task BuildEventsFromData()
		{
			var data = await this.itemService.GetAllItemsAsync();
			var itemCreatedEvents =
				data
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
							item.Spec,
							new List<string>()))
					.ToList();

			var photoAddedEvents =
				data
					.SelectMany(item => item.Photos.Where(photo => photo != "placeholder.jpg").Select((photo, index) => 
						new PhotoAdded(item.Id, index + 2, photo)))
					.ToList();

			itemCreatedEvents.ForEach(item => this.eventStore.StoreAggregateEvent(item, nameof(this.BuildEventsFromData)));
			photoAddedEvents.ForEach(item => this.eventStore.StoreAggregateEvent(item, nameof(this.BuildEventsFromData)));
		}
	}
}