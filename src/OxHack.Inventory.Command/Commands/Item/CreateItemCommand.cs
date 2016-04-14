using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemView = OxHack.Inventory.Query.Models.Item;

namespace OxHack.Inventory.Command.Commands.Item
{
    public class CreateItemCommand : CommandBase<ItemView>
    {
        public CreateItemCommand(ItemView payload) : base(Guid.NewGuid(), payload)
        {
        }
    }
}
