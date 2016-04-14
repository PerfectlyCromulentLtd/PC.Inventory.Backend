﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Commands.Item
{
    public class ChangeAppearanceCommand : CommandBase<string>
    {
        public ChangeAppearanceCommand(Guid id, string payload) : base(id, payload)
        {
        }
    }
}
