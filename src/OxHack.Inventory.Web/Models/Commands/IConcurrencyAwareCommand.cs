using OxHack.Inventory.Web.Services;
using DomainCommands = OxHack.Inventory.Cqrs.Commands;

namespace OxHack.Inventory.Web.Models.Commands
{
    public interface IConcurrencyAwareCommand : ICommand
    {
        string ConcurrencyId
        {
            get;
        }

        DomainCommands.ICommand ToDomainCommand(EncryptionService encryptionService);
    }
}
