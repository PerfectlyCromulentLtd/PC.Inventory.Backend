using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeCurrentLocationCommand : ICommand, IConcurrencyAware, IMapToEvent<CurrentLocationChanged>
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

        public CurrentLocationChanged GetEvent()
        {
            return new CurrentLocationChanged(this.AggregateRootId, this.ConcurrencyId, this.CurrentLocation);
        }
    }
}
