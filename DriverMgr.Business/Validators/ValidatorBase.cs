using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverMgr.Business.Validators
{
    public class ValidatorBase
    {
        public void Validate()
        {
            var context = new ValidationContext(this, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(this, context, results, true);

            var validationDictionary = results
                .Where(w => !string.IsNullOrWhiteSpace(w.ErrorMessage))
                .ToDictionary(k => k.MemberNames.First(), v => v.ErrorMessage);

            if (validationDictionary.Any())
            {
                throw new ValidationException(validationDictionary);
            }
        }
    }
}
