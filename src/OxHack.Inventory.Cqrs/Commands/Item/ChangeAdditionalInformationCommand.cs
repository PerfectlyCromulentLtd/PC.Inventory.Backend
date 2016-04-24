using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeAdditionalInformationCommand : ICommand, IConcurrencyAware
    {
        public ChangeAdditionalInformationCommand(Guid aggregateRootId, Guid concurrencyId, string additionalInformation)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.AdditionalInformation = additionalInformation;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public Guid ConcurrencyId
        {
            get;
        }

        public string AdditionalInformation
        {
            get;
        }
    }
}
