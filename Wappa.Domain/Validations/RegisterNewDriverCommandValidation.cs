
using Wappa.Domain.Commands;

namespace Wappa.Domain.Validations
{
    public class RegisterNewDriverCommandValidation : DriverValidation<RegisterNewDriverCommand>
    {
        public RegisterNewDriverCommandValidation()
        {
            ValidateName();
            ValidateLastName();
            ValidateAddress();
            ValidateCarBrand();
            ValidateCarModel();
            ValidateCarPlate();                    
        }
    }
}