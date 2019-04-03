using System;
using Wappa.Domain.Validations;

namespace Wappa.Domain.Commands
{
    public class RemoveDriverCommand : DriverCommand
    {
        public RemoveDriverCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveDriverCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}