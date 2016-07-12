using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
	public class MigrationController : Controller
	{
		private readonly IItemRepository itemRepository;
		private readonly IEventStore eventStore;

		public MigrationController(IHostingEnvironment environment, IItemRepository itemRepository, IEventStore eventStore)
		{
			if (!environment.IsDevelopment())
			{
				throw new InvalidOperationException("This controller cannot be used outside of Development.");
			}

			this.itemRepository = itemRepository;
			this.eventStore = eventStore;
		}

		[HttpGet]
		[Obsolete("Untested")]
		public async Task<dynamic> DoStuff(
			[FromQuery] bool generateEventsFromQueryDb = false,
			[FromQuery] bool persistResults = false)
		{
			dynamic result = "Nothing";

			if (generateEventsFromQueryDb)
			{
				var items = (await this.itemRepository.GetAllItemsAsync()).ToList();

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
							item.Photos.Select((photo, index) => new PhotoAdded(item.Id, index + 2, photo)))
						.ToList();

				var allEvents =
					itemCreationEvents
						.Cast<IEvent>()
						.Concat(photoAddedEvents);

				if (persistResults)
				{
					foreach (var @event in result)
					{
						this.eventStore.StoreEvent(@event);
					}
				}

				result = allEvents.Select(entry => new
				{
					Type = entry.GetType().Name,
					Body = entry
				});
			}

			return result;
		}
	}
}