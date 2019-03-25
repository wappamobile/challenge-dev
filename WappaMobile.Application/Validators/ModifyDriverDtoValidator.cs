using System.Text.RegularExpressions;
using FluentValidation;

namespace WappaMobile.Application
{
    /// <summary>
    /// Validator for the <see cref="ModifyDriverDto"/>.
    /// </summary>
    public class ModifyDriverDtoValidator : AbstractValidator<ModifyDriverDto>
    {
        public ModifyDriverDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(0, 255);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(0, 255);

            RuleFor(x => x.CarBrand)
                .NotEmpty()
                .Length(0, 255);
            RuleFor(x => x.CarModel)
                .NotEmpty()
                .Length(0, 255);
            RuleFor(x => x.CarRegistrationPlate)
                .NotEmpty()
                .Length(0, 10);

            RuleFor(x => x.AddressLine1)
                .NotEmpty()
                .Length(0, 255);
            RuleFor(x => x.AddressMunicipality)
                .NotEmpty()
                .Length(0, 255);
            RuleFor(x => x.AddressState)
                .NotEmpty()
                .Length(2);
            RuleFor(x => x.AddressZipCode)
                .NotEmpty()
                .Matches(new Regex("[0-9]{5}\\-[0-9]{3}")).WithMessage("Must be in the format '00000-000'.");
        }
    }
}
