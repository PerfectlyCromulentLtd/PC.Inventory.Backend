using OxHack.Inventory.Cqrs.Commands;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs
{
    public class InMemoryBus : IBus
    {
        private Dictionary<Type, Action<ICommand>> commandHandlersByType;
        private Dictionary<Type, List<Action<IEvent>>> eventHandlersByType;
        private readonly IEventStore eventStore;

        public InMemoryBus(IEventStore eventStore)
        {
            this.eventStore = eventStore;
            this.commandHandlersByType = new Dictionary<Type, Action<ICommand>>();
            this.eventHandlersByType = new Dictionary<Type, List<Action<IEvent>>>();
        }

        public async Task IssueCommandAsync<TMessage>(TMessage command) where TMessage : ICommand
        {
            var type = command.GetType();

            Action<ICommand> handler;
            if (this.commandHandlersByType.TryGetValue(type, out handler))
            {
                await Task.Run(() => handler(command));
            }
        }

        public async Task RaiseEventAsync<TMessage>(TMessage @event) where TMessage : IEvent
        {
            this.eventStore.StoreEvent(@event);

            await this.InvokeEventHandlers<TMessage>(@event);
		}

		public async Task ReplayEventAsync<TMessage>(TMessage @event) where TMessage : IEvent
		{
			await this.InvokeEventHandlers<TMessage>(@event);
		}

		private async Task InvokeEventHandlers<TMessage>(TMessage @event) where TMessage : IEvent
        {
            var type = @event.GetType();

            List<Action<IEvent>> handlers;
            if (this.eventHandlersByType.TryGetValue(type, out handlers))
            {
                var tasks = handlers.Select(handler => Task.Run(() => handler(@event)));

                await Task.WhenAll(tasks);
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

            List<Action<IEvent>> eventHandlers;
            if (!this.eventHandlersByType.TryGetValue(type, out eventHandlers))
            {
                eventHandlers = new List<Action<IEvent>>();
                this.eventHandlersByType.Add(type, eventHandlers);
            }

            eventHandlers.Add(DelegateAdjuster.CastArgument<IEvent, TMessage>(@event => handler.Handle(@event)));
        }
	}
}
