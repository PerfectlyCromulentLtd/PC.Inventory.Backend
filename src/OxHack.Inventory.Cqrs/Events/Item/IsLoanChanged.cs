using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class IsLoanChanged : IEvent, IConcurrencyAware
    {
        public IsLoanChanged(Guid aggregateRootId, int concurrencyId, bool isLoan)
        {
            this.AggregateRootId = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.IsLoan = isLoan;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public bool IsLoan
        {
            get;
        }
    }
}
