using OxHack.Inventory.Web.Extensions;
using OxHack.Inventory.Web.Services;
using System;
using DomainCommands = OxHack.Inventory.Cqrs.Commands;

namespace OxHack.Inventory.Web.Models.Commands.Item
{
    public class ChangeAdditionalInformationCommand : IConcurrencyAwareCommand
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

        public string AdditionalInformation
        {
            get;
            set;
        }

        public DomainCommands.ICommand ToDomainCommand(EncryptionService encryptionService, dynamic issuerMetadata)
        {
            return new DomainCommands.Item.ChangeAdditionalInformationCommand(this.Id, this.GetDecryptedConcurrencyId(encryptionService), this.AdditionalInformation, issuerMetadata);
        }
    }
}
