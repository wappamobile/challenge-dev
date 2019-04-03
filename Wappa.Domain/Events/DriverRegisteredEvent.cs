using System;
using Wappa.Domain.Core.Events;

namespace Wappa.Domain.Events
{
    public class DriverRegisteredEvent : Event
    {
        public DriverRegisteredEvent(Guid id, string name, string lastName, string carModel, string carBrand,
            string carPlate, string zipCode, string adress, string coordinates)
        {
            Id = id;
            Name = name;
            LastName = LastName;
            CarModel = carModel;
            CarBrand = carBrand;
            CarPlate = carPlate;
            Zipcode = zipCode;
            Address = adress;
            Coordinates = Coordinates;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string CarModel { get; set; }

        public string CarBrand { get; set; }

        public string CarPlate { get; set; }

        public string Zipcode { get; set; }

        public string Address { get; set; }

        public string Coordinates { get; set; }
    }
}