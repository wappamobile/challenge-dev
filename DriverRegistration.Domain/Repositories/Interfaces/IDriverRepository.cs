using DriverRegistration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DriverRegistration.Domain.Repositories.Interfaces
{
    public interface IDriverRepository
    {
        Driver Get(string id);
        List<Driver> GetAll();
        Task<Driver> Insert(Driver driver);
        Task Update(string id, Driver driver);
        Task Delete(Driver driver);
        Task Delete(string id);
    }
}
