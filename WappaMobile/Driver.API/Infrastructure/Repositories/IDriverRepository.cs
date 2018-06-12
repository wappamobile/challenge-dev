using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WappaMobile.Driver.API.Model;

namespace WappaMobile.Driver.API.Infrastructure.Repositories
{
    public interface IDriverRepository
    {
        List<DriverRegistry> Get();

        DriverRegistry Get(string id);

        void Add(DriverRegistry driver);

        void Update(DriverRegistry driver);

        void Delete(string id);
    }
}
