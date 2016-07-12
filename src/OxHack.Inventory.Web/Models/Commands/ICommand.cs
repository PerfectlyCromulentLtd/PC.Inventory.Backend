using OxHack.Inventory.Web.Services;
using System;
using DomainCommands = OxHack.Inventory.Cqrs.Commands;

namespace OxHack.Inventory.Web.Models.Commands
{
	public interface ICommand
    {
        Guid Id
        {
            get;
		}

		DomainCommands.ICommand ToDomainCommand(EncryptionService encryptionService);
	}
}
