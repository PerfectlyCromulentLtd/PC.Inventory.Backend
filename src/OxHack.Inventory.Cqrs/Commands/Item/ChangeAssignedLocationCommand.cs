using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeAssignedLocationCommand : ICommand, IConcurrencyAware, IMapToEvent<AssignedLocationChanged>
    {
        public ChangeAssignedLocationCommand(Guid aggregateRootId, int concurrencyId, string assignedLocation, dynamic issuerMetadata)
		{
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.AssignedLocation = assignedLocation;
			this.IssuerMetadata = issuerMetadata;
		}

        public Guid Id
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

		public dynamic IssuerMetadata
		{
			get;
		}

		public AssignedLocationChanged GetEvent()
        {
            return new AssignedLocationChanged(this.Id, this.ConcurrencyId + 1, this.AssignedLocation);
        }
    }
}
