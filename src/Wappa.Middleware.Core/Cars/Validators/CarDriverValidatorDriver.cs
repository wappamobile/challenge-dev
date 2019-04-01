using Wappa.Middleware.Domain.Drivers;
using DotNetCore.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Wappa.Middleware.Core.Cars
{
    public sealed class CarValidatorDriver : AbstractValidator<Driver>
    {
        

        public override ValidationResult Validate(ValidationContext<Driver> driver)
        {
            return driver.InstanceToValidate == null
                ? new ValidationResult(new[] { new ValidationFailure("Driver", "Motorista nao cadastrado.") })
                : base.Validate(driver);
        }
    }
}
