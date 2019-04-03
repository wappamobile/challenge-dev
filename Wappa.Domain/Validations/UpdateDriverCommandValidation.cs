using Wappa.Domain.Commands;
using Wappa.Domain.Validations;

namespace Wappa.Domain.Validations
{
    public class UpdateDriverCommandValidation : DriverValidation<UpdateDriverCommand>
    {
        public UpdateDriverCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateLastName();
            ValidateAddress();
            ValidateCarBrand();
            ValidateCarModel();
            ValidateCarPlate();            
        }
    }
}