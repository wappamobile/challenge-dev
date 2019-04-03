using System;
using Wappa.Domain.Core.Models;

namespace Wappa.Domain.Models
{
    public class Driver : Entity
    {
        public Driver() { }

        public Driver(Guid id, string name, string lastName, string carModel, string carBrand, string carPlate,
            string zipcode, string address, string coordinates)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CarModel = carModel;
            CarBrand = carBrand;
            CarPlate = carPlate;
            Zipcode = zipcode;
            Address = address;
            Coordinates = coordinates;

        }

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
