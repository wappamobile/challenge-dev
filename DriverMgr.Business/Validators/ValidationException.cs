using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverMgr.Business.Validators
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(IDictionary<string, string> fieldsErrors) : base("Some validation requirements was not fullfilled. " + fieldsErrors.Aggregate("", (ac, i) => ac + ". " + i.Value).Substring(2))
        {
            FieldsErrors = fieldsErrors;
        }

        public IDictionary<string, string> FieldsErrors { get; }
    }
}
