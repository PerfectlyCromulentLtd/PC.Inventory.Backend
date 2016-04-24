using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeCurrentLocationCommand : ICommand, IConcurrencyAware
    {
        public ChangeCurrentLocationCommand(Guid aggregateRootId, Guid concurrencyId, string currentLocation)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.CurrentLocation = currentLocation;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string CurrentLocation
        {
            get;
        }
    }
}
