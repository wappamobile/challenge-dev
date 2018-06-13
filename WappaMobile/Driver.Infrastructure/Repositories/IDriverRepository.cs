using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WappaMobile.Driver.API.Model;

namespace WappaMobile.Driver.Infrastructure.Repositories
{
    public interface IDriverRepository
    {
        List<DriverRegistry> Get();

        List<DriverRegistry> GetPendingGeolocation();

        DriverRegistry Get(string id);

        bool Add(DriverRegistry driver);

        bool Update(DriverRegistry driver);

        bool Delete(string id);
    }
}
