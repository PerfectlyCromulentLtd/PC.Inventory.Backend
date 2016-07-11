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
using System.Dynamic;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Query.Repositories;

namespace OxHack.Inventory.Web.Controllers
{
	[Obsolete("This is a debugging tool.  It should NOT go out to production.")]
	[Route("api/[controller]")]
	public class MigrationController : Controller
	{
		private readonly IItemRepository itemRepository;
		private readonly IBus bus;

		public MigrationController(IHostingEnvironment environment, IItemRepository itemRepository, IBus bus)
		{
			if (!environment.IsDevelopment())
			{
				throw new InvalidOperationException("This controller cannot be used outside of Development.");
			}

			this.itemRepository = itemRepository;
			this.bus = bus;
		}

		[HttpGet]
		public async Task<dynamic> GetAll([FromQuery] bool generateEventsFromQueryDb = false, [FromQuery] bool persistResults = false)
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

				List<PhotoAdded> photoAddedEvents = new List<PhotoAdded>();

				if (persistResults)
				{
					foreach (var creationEvent in itemCreationEvents)
					{
						await this.bus.RaiseEventAsync(creationEvent);
						var createdItem = this.itemRepository.GetItemByIdAsync(creationEvent.AggregateRootId);

						var itemPhotoAddedEvents =
							items
								.
					}
				}
				else
				{
					photoAddedEvents =
						items
							.SelectMany(item =>
								item.Photos.Select(photo => new PhotoAdded(item.Id, Guid.NewGuid(), photo)))
							.ToList();
				}

				result = itemCreationEvents.Cast<IEvent>().Concat(photoAddedEvents).Select(entry => new { Type = entry.GetType().Name, Body = entry });
			}

			return result;
		}
	}
}