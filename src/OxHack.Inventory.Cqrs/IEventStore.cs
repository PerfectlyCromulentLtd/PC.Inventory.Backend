using OxHack.Inventory.Cqrs.Events;

namespace OxHack.Inventory.Cqrs
{
    public interface IEventStore
    {
        void StoreEvent(IEvent message);
    }
}