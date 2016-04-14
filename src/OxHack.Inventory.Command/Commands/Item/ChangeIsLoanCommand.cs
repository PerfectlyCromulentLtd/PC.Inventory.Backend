using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Commands.Item
{
    public class ChangeIsLoanCommand : CommandBase<bool>
    {
        public ChangeIsLoanCommand(Guid id, bool payload) : base(id, payload)
        {
        }
    }
}
