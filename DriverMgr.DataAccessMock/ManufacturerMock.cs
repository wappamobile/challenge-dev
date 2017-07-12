using DriverMgr.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverMgr.TO;

namespace DriverMgr.DataAccessMock
{
    public class ManufacturerMock : IManufacturerDAL
    {
        private readonly List<ManufacturerTO> _manufacturers = new List<ManufacturerTO>();

        public ManufacturerMock()
        {
            _manufacturers.Add(new ManufacturerTO
            {
                ManufacturerId = 1,
                Name = "Nissan"
            });

            _manufacturers.Add(new ManufacturerTO
            {
                ManufacturerId = 2,
                Name = "Mitsubishi"
            });

            _manufacturers.Add(new ManufacturerTO
            {
                ManufacturerId = 3,
                Name = "Aston"
            });
        }

        public IEnumerable<ManufacturerTO> LIst()
        {
            // ToList protects the internal list from external alterations.
            return _manufacturers.ToList();
        }
    }
}
