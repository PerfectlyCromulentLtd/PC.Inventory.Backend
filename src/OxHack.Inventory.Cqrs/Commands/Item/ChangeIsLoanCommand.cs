using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeIsLoanCommand : ICommand, IConcurrencyAware, IMapToEvent<IsLoanChanged>
    {
        public ChangeIsLoanCommand(Guid aggregateRootId, int concurrencyId, bool isLoan)
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

        public IsLoanChanged GetEvent()
        {
            return new IsLoanChanged(this.AggregateRootId, this.ConcurrencyId + 1, this.IsLoan);
        }
    }
}
