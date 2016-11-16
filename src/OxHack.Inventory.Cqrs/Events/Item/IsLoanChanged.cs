using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class IsLoanChanged : IAggregateEvent
	{
        public IsLoanChanged(Guid id, int concurrencyId, bool isLoan)
        {
            this.Id = id;
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
