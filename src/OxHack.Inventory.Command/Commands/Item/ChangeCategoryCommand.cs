using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Commands.Item
{
    public class ChangeCategoryCommand : CommandBase<string>
    {
        public ChangeCategoryCommand(Guid id, string payload) : base(id, payload)
        {
        }
    }
}
