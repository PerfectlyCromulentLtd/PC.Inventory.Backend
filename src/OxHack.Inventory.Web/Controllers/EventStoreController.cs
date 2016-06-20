using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Controllers
{
	[Obsolete("This is a debugging tool.  It should NOT go out to production.")]
	[Route("api/[controller]")]
	public class EventStoreController : Controller
	{
		private readonly IEventStore eventStore;
		private readonly IBus bus;
		private readonly IItemRepository itemRepository;

		public EventStoreController(
			IHostingEnvironment environment, 
			IEventStore eventStore, 
			IBus bus, 
			IItemRepository itemRepository)
		{
			if (!environment.IsDevelopment())
			{
				throw new InvalidOperationException("This controller cannot be used outside of Development.");
			}

			this.eventStore = eventStore;
			this.bus = bus;
			this.itemRepository = itemRepository;
		}

		[HttpGet("{aggregateId}")]
		public dynamic GetByAggregateId(Guid aggregateId, [FromQuery] bool replay = false)
		{
			var events = this.eventStore.GetEventsByAggregateId(aggregateId);

			if (replay)
			{
				this.ReplayEvents(events);
			}

			return events;
		}

		[HttpGet]
		public async Task<dynamic> GetAll([FromQuery] bool replay = false, [FromQuery] bool buildEventsFromData = false)
		{
			if (replay && buildEventsFromData)
			{
				return this.HttpBadRequest($"{nameof(replay)} and {nameof(buildEventsFromData)} flags are mutually exclusive.");
			}

			if (buildEventsFromData)
			{
				await this.BuildEventsFromData();
			}

			var events = this.eventStore.GetAllEvents();

			if (replay)
			{
				await this.ReplayEvents(events);
			}

			return events.Select(item => new
			{
				Type = item.Event.GetType().Name,
				EventInfo = item
			});
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
			var data = await this.itemRepository.GetAllItemsAsync();
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
							item.Spec))
					.ToList();

			var photoAddedEvents =
				data
					.SelectMany(item => item.Photos.Where(photo => photo != "placeholder.jpg").Select((photo, index) => 
						new PhotoAdded(item.Id, index + 2, photo)))
					.ToList();

			itemCreatedEvents.ForEach(item => this.eventStore.StoreEvent(item));
			photoAddedEvents.ForEach(item => this.eventStore.StoreEvent(item));
		}
	}
}