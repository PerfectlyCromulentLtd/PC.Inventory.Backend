using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Models;
using OxHack.Inventory.Web.Extensions;
using Microsoft.Extensions.Configuration;
using OxHack.Inventory.Web.Models.Commands.Item;
using OxHack.Inventory.Web.Services;
using System.Net;
using OxHack.Inventory.Web.Models.Commands;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using Microsoft.AspNet.Hosting;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Query.Repositories;
using OxHack.Inventory.Cqrs.Events.Item;

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

			var events = await Task.FromResult(this.eventStore.GetAllEvents());

			if (replay)
			{
				var eventsToReplay = events.OrderBy(item => item.CommitStamp).Select(item => item.Event).ToList();

				var replayTasks = eventsToReplay.Select(@event => this.bus.ReplayEventAsync(@event)).ToList();

				await Task.WhenAll(replayTasks);
			}

			return events.Select(item => new
			{
				Type = item.Event.GetType().Name,
				EventInfo = item
			});
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