using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverMgr.DataAccess.Factory;
using DriverMgr.DataAccess.Contracts;
using DriverMgr.TO;

namespace DriverMgr.Business.Core
{
    public class ManufacturerBL : BaseBL
    {
        private IManufacturerDAL _dal;

        public ManufacturerBL(DataFactory factory) : base(factory)
        {
            _dal = factory.GetDAL<IManufacturerDAL>();
        }

        public IEnumerable<ManufacturerTO> List()
        {
            return _dal.LIst();
        }
    }
}
