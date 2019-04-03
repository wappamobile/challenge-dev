using System;
using Wappa.Domain.Validations;

namespace Wappa.Domain.Commands
{
    public class UpdateDriverCommand : DriverCommand
    {

        public UpdateDriverCommand(Guid id, string name, string lastName, string carModel, string carBrand,
            string carPlate, string zipCode, string address, string coordinates)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            CarModel = carModel;
            CarBrand = carBrand;
            CarPlate = carPlate;
            Zipcode = zipCode;
            Address = address;
            Coordinates = coordinates;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateDriverCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}