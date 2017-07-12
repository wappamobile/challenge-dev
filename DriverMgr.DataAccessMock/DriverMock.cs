using DriverMgr.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverMgr.TO;

namespace DriverMgr.DataAccessMock
{
    public class DriverMock : IDriverDAL
    {
        private readonly List<DriverTO> _drivers = new List<DriverTO>();

        public DriverMock()
        {
            _drivers.Add(new DriverTO
            {
                DriverId = 1,
                FirstName = "John",
                LastName = "Doe",
                ManufacturerId = 1,
                Model = "Skyline",
                Plate = "ABC-1234",
                Address = "Avenida Paulista, 120 - Bela Vista, São Paulo - SP, Brasil",
                Latitude = -23.5699932,
                Longitude = -46.6458453
            });

            _drivers.Add(new DriverTO
            {
                DriverId = 2,
                FirstName = "Marry",
                LastName = "Ane",
                ManufacturerId = 2,
                Model = "Lancer",
                Plate = "XYZ-6789",
                Address = "Avenida Brigadeiro Faria Lima, 1500 - Jardim Paulistano, São Paulo, Brasil",
                Latitude = -23.5707412,
                Longitude = -46.6910824
            });
        }

        public DriverTO Get(long id)
        {
            return _drivers.Single(s => s.DriverId == id);
        }

        public IEnumerable<DriverTO> List()
        {
            // ToList protects the internal list from external alterations.
            return _drivers.ToList();
        }

        public void Create(DriverTO driver)
        {
            driver.DriverId = _drivers.Max(m => m.DriverId) + 1;

            _drivers.Add(driver);
        }

        public void Update(DriverTO driver)
        {
            var entity = _drivers.Single(s => s.DriverId == driver.DriverId);

            entity.DriverId = driver.DriverId;
            entity.FirstName = driver.FirstName;
            entity.LastName = driver.LastName;
            entity.ManufacturerId = driver.ManufacturerId;
            entity.Model = driver.Model;
            entity.Plate = driver.Plate;
            entity.Address = driver.Address;
            entity.Longitude = driver.Longitude;
            entity.Latitude = driver.Latitude;
        }

        public void Delete(long id)
        {
            var driver = _drivers.Single(s => s.DriverId == id);

            _drivers.Remove(driver);
        }
    }
}
