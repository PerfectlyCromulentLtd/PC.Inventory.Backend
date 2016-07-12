using OxHack.Inventory.Cqrs.Commands;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs
{
	public class InMemoryBus : IBus
	{
		private Dictionary<Type, Func<ICommand, Task>> commandHandlersByType;
		private Dictionary<Type, List<Func<IEvent, Task>>> eventHandlersByType;

		public InMemoryBus()
		{
			this.commandHandlersByType = new Dictionary<Type, Func<ICommand, Task>>();
			this.eventHandlersByType = new Dictionary<Type, List<Func<IEvent, Task>>>();
		}

		public async Task IssueCommandAsync<TMessage>(TMessage command) where TMessage : ICommand
		{
			var type = command.GetType();

			Func<ICommand, Task> handler;
			if (this.commandHandlersByType.TryGetValue(type, out handler))
			{
				await handler(command);
			}
		}

		public async Task RaiseEventAsync<TMessage>(TMessage @event) where TMessage : IEvent
		{
			var type = @event.GetType();

			List<Func<IEvent, Task>> handlers;
			if (this.eventHandlersByType.TryGetValue(type, out handlers))
			{
				foreach (var handler in handlers)
				{
					await handler(@event);
				}
			}
		}

		public void RegisterCommandHandler<TMessage>(IHandle<TMessage> handler) where TMessage : ICommand
		{
			var type = typeof(TMessage);
			if (this.commandHandlersByType.ContainsKey(type))
			{
				throw new InvalidOperationException("Only one command handler may be registered per command.");
			}

			this.commandHandlersByType[type] = DelegateAdjuster.CastArgument<ICommand, TMessage>(command => handler.Handle(command));
		}

		public void RegisterEventHandler<TMessage>(IHandle<TMessage> handler) where TMessage : IEvent
		{
			var type = typeof(TMessage);

			List<Func<IEvent, Task>> eventHandlers;
			if (!this.eventHandlersByType.TryGetValue(type, out eventHandlers))
			{
				eventHandlers = new List<Func<IEvent, Task>>();
				this.eventHandlersByType.Add(type, eventHandlers);
			}

			eventHandlers.Add(DelegateAdjuster.CastArgument<IEvent, TMessage>(@event => handler.Handle(@event)));
		}
	}
}
