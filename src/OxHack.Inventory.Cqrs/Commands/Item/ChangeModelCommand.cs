using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeModelCommand : ICommand, IConcurrencyAware, IMapToEvent<ModelChanged>
    {
        public ChangeModelCommand(Guid aggregateRootId, int concurrencyId, string model)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Model = model;
        }

        public Guid AggregateRootId
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

        public ModelChanged GetEvent()
        {
            return new ModelChanged(this.AggregateRootId, this.ConcurrencyId, this.Model);
        }
    }
}
