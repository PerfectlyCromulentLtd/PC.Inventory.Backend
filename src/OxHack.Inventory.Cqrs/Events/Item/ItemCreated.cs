using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class ItemCreated : IEvent
    {
        public ItemCreated(
               Guid aggregateRootId,
               string additionalInformation,
               string appearance,
               string assignedLocation,
               string category,
               string currentLocation,
               bool isLoan,
               string manufacturer,
               string model,
               string name,
               string origin,
               int quantity,
               string spec
               )
        {
            this.AggregateRootId = aggregateRootId;
            this.AdditionalInformation = additionalInformation;
            this.Appearance = appearance;
            this.AssignedLocation = assignedLocation;
            this.Category = category;
            this.CurrentLocation = currentLocation;
            this.IsLoan = isLoan;
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Name = name;
            this.Origin = origin;
            this.Quantity = quantity;
            this.Spec = spec;
        }

        public Guid AggregateRootId
        {
            get;
        }

        public string Name
        {
            get;
        }

        public string Manufacturer
        {
            get;
        }

        public string Model
        {
            get;
        }

        public int Quantity
        {
            get;
        }

        public string Category
        {
            get;
        }

        public string Spec
        {
            get;
        }

        public string Appearance
        {
            get;
        }

        public string AssignedLocation
        {
            get;
        }

        public string CurrentLocation
        {
            get;
        }

        public bool IsLoan
        {
            get;
        }

        public string Origin
        {
            get;
        }

        public string AdditionalInformation
        {
            get;
        }
    }
}
