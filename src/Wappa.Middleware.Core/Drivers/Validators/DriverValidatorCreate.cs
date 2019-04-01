using Wappa.Middleware.Domain.Drivers;
using DotNetCore.Validation;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Wappa.Middleware.Core.Drivers
{
    public sealed class DriverValidatorCreate : Validator<Driver>
    {
        private readonly IEnumerable<Driver> _items;

        public DriverValidatorCreate(IEnumerable<Driver> items)
        {
            _items = items;
            RuleFor(x => x).NotNull();
            RuleFor(x => x.FirtName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.Number).NotNull().NotEmpty();
            RuleFor(x => x.District).NotNull().NotEmpty();
            RuleFor(x => x.ZipCode).NotNull().NotEmpty();
            RuleFor(x => x.City).NotNull().NotEmpty();
            RuleFor(x => x.State).NotNull().NotEmpty();
            RuleFor(x => x).Must(x => !IsNameUnique(x))
                .WithMessage("Motorista ja cadastrado!");
        }

        public bool IsNameUnique(Driver driver)
        {
            return _items.Any(t =>
              t.FirtName.ToLower() == driver.FirtName.ToLower() && t.LastName.ToLower() == driver.LastName.ToLower());
        }
    }
}
