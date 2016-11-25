using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeModelCommand : ICommand, IConcurrencyAware, IMapToEvent<ModelChanged>
    {
        public ChangeModelCommand(Guid aggregateRootId, int concurrencyId, string model, dynamic issuerMetadata)
		{
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Model = model;
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

        public string Model
        {
            get;
		}

		public dynamic IssuerMetadata
		{
			get;
		}

		public ModelChanged GetEvent()
        {
            return new ModelChanged(this.Id, this.ConcurrencyId + 1, this.Model);
        }
    }
}
