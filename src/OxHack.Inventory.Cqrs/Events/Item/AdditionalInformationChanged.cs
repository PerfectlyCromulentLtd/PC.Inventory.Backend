using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class AdditionalInformationChanged : IEvent, IConcurrencyAware
    {
        public AdditionalInformationChanged(Guid aggregateRootId, int concurrencyId, string additionalInformation)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.AdditionalInformation = additionalInformation;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public string AdditionalInformation
        {
            get;
        }
    }
}
