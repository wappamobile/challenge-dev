using System;
using Wappa.Domain.Validations;

namespace Wappa.Domain.Commands
{
    public class RegisterNewDriverCommand : DriverCommand
    {
        public RegisterNewDriverCommand(string name, string lastName, string carModel, string carBrand, 
            string carPlate, string zipCode, string adress, string coordinates)
        {
            Name = name;
            LastName = lastName;
            CarModel = carModel;
            CarBrand = carBrand;
            CarPlate = carPlate;
            Zipcode = zipCode;
            Address = adress;
            Coordinates = coordinates;
            

        }        

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewDriverCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}