using FluentValidation;
using System;
using Wappa.Domain.Commands;
using Wappa.Domain.Models;

namespace Wappa.Domain.Validations
{
    public abstract class DriverValidation<T> : AbstractValidator<T> where T : DriverCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(3, 150).WithMessage("The Name must have between 3 and 150 characters");
        }

        protected void ValidateLastName()
        {
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Please ensure you have entered the LastName")
                .Length(3, 150).WithMessage("The Last Name must have between 3 and 150 characters");
        }

        protected void ValidateCarModel()
        {
            RuleFor(c => c.CarModel)
                .NotEmpty().WithMessage("Please ensure you have entered the Car Model")
                .Length(3, 150).WithMessage("The Car Model must have between 3 and 150 characters");
        }

        protected void ValidateCarBrand()
        {
            RuleFor(c => c.CarBrand)
                .NotEmpty().WithMessage("Please ensure you have entered the Car Brand")
                .Length(3, 150).WithMessage("The Car Brand must have between 3 and 150 characters");
        }

        protected void ValidateCarPlate()
        {
            RuleFor(c => c.CarPlate)
                .NotEmpty().WithMessage("Please ensure you have entered the Car Plate")
                .Length(3, 150).WithMessage("The Car Plate must have between 3 and 150 characters");
        }

        protected void ValidateZipcode()
        {
            RuleFor(c => c.Zipcode)
                .NotEmpty().WithMessage("Please ensure you have entered the Car Plate")
                .Length(9, 150).WithMessage("The Zipcode must have between 9 and 150 characters");
        }

        protected void ValidateAddress()
        {
            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Please ensure you have entered the Address")
                .Length(5, 150).WithMessage("The Address must have between 5 and 150 characters");
        }      

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }    
    }
}
