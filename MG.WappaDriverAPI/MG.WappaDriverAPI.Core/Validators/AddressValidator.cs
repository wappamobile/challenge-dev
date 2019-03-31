using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MG.WappaDriverAPI.Core.Models;

namespace MG.WappaDriverAPI.Core.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.StreetOrAddress).NotEmpty().Length(3, 200);
            RuleFor(x => x.StateOrProvince).NotEmpty().Length(3, 200);
            RuleFor(x => x.City).NotEmpty().Length(3, 200);
            RuleFor(x => x.Country).NotEmpty().Length(3, 200);
            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.Latitude).NotEqual(0);
            RuleFor(x => x.Longitude).NotEqual(0);
        }
    }
}
