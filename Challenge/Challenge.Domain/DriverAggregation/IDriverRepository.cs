using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.DriverAggregation
{
    public interface IDriverRepository
    {
        Task Add(Driver driver);
        Task Update(Driver driver);
        Task Remove(Driver driver);
        Task<Driver> GetById(IQueryableById queryableById);
    }
}
