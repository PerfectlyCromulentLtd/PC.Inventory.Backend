﻿using OxHack.Inventory.Web.Extensions;
using OxHack.Inventory.Web.Services;
using System;
using System.Collections.Generic;
using DomainCommands = OxHack.Inventory.Cqrs.Commands;

namespace OxHack.Inventory.Web.Models.Commands.Item
{
    public class UpdateItemCommand : IConcurrencyAwareCommand
    {
        public Guid Id
        {
            get;
            set;
        }

        public int Version
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Manufacturer
        {
            get;
            set;
        }

        public string Model
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public string Spec
        {
            get;
            set;
        }

        public string Appearance
        {
            get;
            set;
        }

        public string AssignedLocation
        {
            get;
            set;
        }

        public string CurrentLocation
        {
            get;
            set;
        }

        public bool IsLoan
        {
            get;
            set;
        }

        public string Origin
        {
            get;
            set;
        }

        public string AdditionalInformation
        {
            get;
            set;
        }

        public List<string> Photos
        {
            get;
            set;
        }

        public string ConcurrencyId
        {
            get;
            set;
        }

        public DomainCommands.ICommand ToDomainCommand(EncryptionService encryptionService)
        {
            return
                new DomainCommands.Item.UpdateItemCommand(
                    this.Id == Guid.Empty ? Guid.NewGuid() : this.Id,
                    this.AdditionalInformation,
                    this.Appearance,
                    this.AssignedLocation,
                    this.Category,
                    this.CurrentLocation,
                    this.IsLoan,
                    this.Manufacturer,
                    this.Model,
                    this.Name,
                    this.Origin,
                    this.Quantity,
                    this.Spec,
                    this.Photos,
					this.GetDecryptedConcurrencyId(encryptionService));
        }
    }
}
