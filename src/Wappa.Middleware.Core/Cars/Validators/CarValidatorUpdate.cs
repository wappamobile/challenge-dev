using Wappa.Middleware.Domain.Cars;
using DotNetCore.Validation;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Wappa.Middleware.Core.Cars
{
    public sealed class CarValidatorUpdate : Validator<Car>
    {
        private readonly IEnumerable<Car> _items;

        public CarValidatorUpdate(IEnumerable<Car> items)
        {
            _items = items;
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Brand).NotNull().NotEmpty();
            RuleFor(x => x.Model).NotNull().NotEmpty();
            RuleFor(x => x.Plate).NotNull().NotEmpty();
            RuleFor(x => x).Must(x => !IsNameUnique(x))
            .WithMessage("Carro ja cadastrado!");
        }

        public bool IsNameUnique(Car car)
        {
            var result = _items.Any(t =>
              t.Plate == car.Plate && t.Id != car.Id);

            return result;
        }
    }
}
