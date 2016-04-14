using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Commands.Item
{
    public class ChangeNameCommand : CommandBase<string>
    {
        public ChangeNameCommand(Guid id, string payload) : base(id, payload)
        {
        }
    }
}
