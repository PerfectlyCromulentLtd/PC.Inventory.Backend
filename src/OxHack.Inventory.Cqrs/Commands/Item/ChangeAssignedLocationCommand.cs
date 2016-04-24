using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeAssignedLocationCommand : ICommand, IConcurrencyAware
    {
        public ChangeAssignedLocationCommand(Guid aggregateRootId, Guid concurrencyId, string assignedLocation)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.AssignedLocation = assignedLocation;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string AssignedLocation
        {
            get;
        }
    }
}
