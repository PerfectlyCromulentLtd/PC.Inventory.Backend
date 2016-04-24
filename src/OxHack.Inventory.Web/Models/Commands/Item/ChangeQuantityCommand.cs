using OxHack.Inventory.Web.Extensions;
using OxHack.Inventory.Web.Services;
using System;
using DomainCommands = OxHack.Inventory.Cqrs.Commands;

namespace OxHack.Inventory.Web.Models.Commands.Item
{
    public class ChangeQuantityCommand : IConcurrencyAwareCommand
    {
        public Guid Id
        {
            get;
            set;
        }

        public string ConcurrencyId
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

        public DomainCommands.ICommand ToDomainCommand(EncryptionService encryptionService)
        {
            return
                new DomainCommands.Item.ChangeQuantityCommand(
                    this.Id,
                    this.GetDecryptedConcurrencyId(encryptionService),
                    this.Quantity);
        }
    }
}
