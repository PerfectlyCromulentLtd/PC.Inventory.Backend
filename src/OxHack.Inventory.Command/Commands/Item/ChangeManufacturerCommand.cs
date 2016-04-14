using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Commands.Item
{
    public class ChangeManufacturerCommand : CommandBase<string>
    {
        public ChangeManufacturerCommand(Guid id, string payload) : base(id, payload)
        {
        }
    }
}
