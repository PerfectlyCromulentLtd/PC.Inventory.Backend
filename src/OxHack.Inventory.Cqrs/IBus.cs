using OxHack.Inventory.Cqrs.Commands;
using OxHack.Inventory.Cqrs.Events;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs
{
    public interface IBus
    {
        Task IssueCommandAsync<TMessage>(TMessage command) where TMessage : ICommand;
        Task RaiseEventAsync<TMessage>(TMessage @event) where TMessage : IEvent;
		Task ReplayEventAsync<TMessage>(TMessage @event) where TMessage : IEvent;

		void RegisterCommandHandler<TMessage>(IHandle<TMessage> handler) where TMessage : ICommand;
        void RegisterEventHandler<TMessage>(IHandle<TMessage> handler) where TMessage : IEvent;
    }
}