using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Commands.Item
{
    public class ChangeQuantityCommand : CommandBase<int>
    {
        public ChangeQuantityCommand(Guid id, int payload) : base(id, payload)
        {
        }
    }
}
