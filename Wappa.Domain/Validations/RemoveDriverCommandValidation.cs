using Wappa.Domain.Commands;
using Wappa.Domain.Validations;

namespace Wappa.Domain.Validations
{
    public class RemoveDriverCommandValidation : DriverValidation<RemoveDriverCommand>
    {
        public RemoveDriverCommandValidation()
        {
            ValidateId();
        }
    }
}