using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Domain.DriverAggregation
{
    public class AddDriverSpecification : AbstractValidator<AddDriverDto>
    {
        public AddDriverSpecification()
        {

        }
    }
}
