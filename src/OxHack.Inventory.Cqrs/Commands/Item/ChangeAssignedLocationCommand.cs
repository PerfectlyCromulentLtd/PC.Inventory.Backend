using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeAssignedLocationCommand : ICommand, IConcurrencyAware, IMapToEvent<AssignedLocationChanged>
    {
        public ChangeAssignedLocationCommand(Guid aggregateRootId, int concurrencyId, string assignedLocation)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.AssignedLocation = assignedLocation;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string AssignedLocation
        {
            get;
        }

        public AssignedLocationChanged GetEvent()
        {
            return new AssignedLocationChanged(this.AggregateRootId, this.ConcurrencyId + 1, this.AssignedLocation);
        }
    }
}
