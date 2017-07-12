using DriverMgr.TO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverMgr.DataAccess.Contracts
{
    public interface IManufacturerDAL
    {
        IEnumerable<ManufacturerTO> LIst();

        // For this example no other methods are needed.
    }
}
