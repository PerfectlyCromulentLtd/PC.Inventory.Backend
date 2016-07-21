using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeCurrentLocationCommand : ICommand, IConcurrencyAware, IMapToEvent<CurrentLocationChanged>
    {
        public ChangeCurrentLocationCommand(Guid aggregateRootId, int concurrencyId, string currentLocation)
        {
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.CurrentLocation = currentLocation;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string CurrentLocation
        {
            get;
        }

        public CurrentLocationChanged GetEvent()
        {
            return new CurrentLocationChanged(this.Id, this.ConcurrencyId + 1, this.CurrentLocation);
        }
    }
}
