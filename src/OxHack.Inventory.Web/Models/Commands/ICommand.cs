using System;

namespace OxHack.Inventory.Web.Models.Commands
{
    public interface ICommand
    {
        Guid Id
        {
            get;
        }
    }
}
