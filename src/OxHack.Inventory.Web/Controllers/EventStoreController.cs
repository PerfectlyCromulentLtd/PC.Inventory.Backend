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

namespace OxHack.Inventory.Web.Controllers
{
	[Obsolete("This is a debugging tool.  It should NOT go out to production.")]
	[Route("api/[controller]")]
	public class EventStoreController : Controller
	{
		private readonly IEventStore eventStore;
		private readonly IBus bus;

		public EventStoreController(IHostingEnvironment environment, IEventStore eventStore, IBus bus)
		{
			if (!environment.IsDevelopment())
			{
				throw new InvalidOperationException("This controller cannot be used outside of Development.");
			}

			this.eventStore = eventStore;
			this.bus = bus;
		}

		[HttpGet]
		public async Task<dynamic> GetAll([FromQuery] bool replay = false)
		{
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

		//[HttpGet("{id}")]
		//public async Task<Item> GetById(Guid id)
		//{
		//    var model = await this.itemService.GetItemByIdAsync(id);

		//    return model.ToWebModel(this.Host + this.config["PathTo:ItemPhotos"], this.encryptionService);
		//}
	}
}