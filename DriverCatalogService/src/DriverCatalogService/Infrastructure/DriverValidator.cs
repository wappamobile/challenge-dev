using System.Collections.Generic;
using System.Linq;
using DriverCatalogService.Models;

namespace DriverCatalogService.Infrastructure
{
    public class DriverValidator
    {
        private readonly IRepository _repository;

        public DriverValidator(IRepository repository)
        {
            _repository = repository;
        }

        public List<Error> ValidateForCreation(Driver driver)
        {
            var res = ValidateFields(driver);

            if (!res.Any() && _repository.Exists(driver.Name))
            {
                res.Add(new Error { Problem = "Already exists", Where = nameof(Driver)});
            }

            return res;
        }

        public List<Error> ValidateFields(Driver driver)
        {
            var res = new List<Error>();

            if (string.IsNullOrEmpty(driver.Name.FirstName))
            {
                res.Add(new Error {Problem = "Cannot be empty", Where = $"{nameof(Driver.Name)}.{nameof(Driver.Name.FirstName)}"});
            }

            if (string.IsNullOrEmpty(driver.Name.LastName))
            {
                res.Add(new Error {Problem = "Cannot be empty", Where = $"{nameof(Driver.Name)}.{nameof(Driver.Name.LastName)}"});
            }

            return res;
        }

        public List<Error> ValidateForUpdate(Driver driver)
        {
            var res = ValidateFields(driver);

            if (!res.Any() && _repository.ContainsAnother(driver.Id, driver.Name))
            {
                res.Add(new Error { Problem = "Name taken", Where = nameof(Driver)});
            }

            return res;
        }
    }
}