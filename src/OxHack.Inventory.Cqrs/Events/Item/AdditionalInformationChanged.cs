using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class AdditionalInformationChanged : IEvent, IConcurrencyAware
    {
        public AdditionalInformationChanged(Guid aggregateRootId, Guid concurrencyId, string additionalInformation)
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

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.AdditionalInformation = this.AdditionalInformation;

            return aggregate;
        }
    }
}
