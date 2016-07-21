using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeIsLoanCommand : ICommand, IConcurrencyAware, IMapToEvent<IsLoanChanged>
    {
        public ChangeIsLoanCommand(Guid aggregateRootId, int concurrencyId, bool isLoan)
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

        public IsLoanChanged GetEvent()
        {
            return new IsLoanChanged(this.Id, this.ConcurrencyId + 1, this.IsLoan);
        }
    }
}
