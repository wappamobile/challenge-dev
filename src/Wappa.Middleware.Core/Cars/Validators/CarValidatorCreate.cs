using Wappa.Middleware.Domain.Cars;
using DotNetCore.Validation;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Wappa.Middleware.Core.Cars
{
    public sealed class CarValidatorCreate : Validator<Car>
    {
        private readonly IEnumerable<Car> _items;

        public CarValidatorCreate(IEnumerable<Car> items)
        {
            _items = items;
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Brand).NotNull().NotEmpty();
            RuleFor(x => x.Model).NotNull().NotEmpty();
            RuleFor(x => x.Plate).NotNull().NotEmpty();
            RuleFor(x => x).Must(x=> !IsNameUnique(x.Plate))
            .WithMessage("Carro ja cadastrado!");
        }

        public bool IsNameUnique(string plate)
        {
            return _items.Any(x => x.Plate == plate);
        }
    }
}
