using DriverMgr.DataAccess.Contracts;
using DriverMgr.DataAccess.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverMgr.DataAccessMock
{
    public class DataFactoryMock : DataFactory
    {
        private DriverMock _driver = new DriverMock();
        private ManufacturerMock _manufacturer = new ManufacturerMock();

        public override T GetDAL<T>()
        {
            var ttype = typeof(T);

            if (ttype == typeof(IDriverDAL))
            {
                return (T)(object)_driver;
            }

            if (ttype == typeof(IManufacturerDAL))
            {
                return (T)(object)_manufacturer;
            }

            throw new NotImplementedException($"Mock for {ttype.Name} was not implemented.");
        }
    }
}