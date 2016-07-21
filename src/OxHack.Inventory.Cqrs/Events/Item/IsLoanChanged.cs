using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class IsLoanChanged : IEvent
    {
        public IsLoanChanged(Guid aggregateRootId, int concurrencyId, bool isLoan)
        {
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.IsLoan = isLoan;
        }

        public Guid Id
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

        public dynamic Apply(dynamic aggregate)
        {
            aggregate.IsLoan = this.IsLoan;

            return aggregate;
        }
    }
}
