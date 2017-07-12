using System;
using System.Collections.Generic;
using System.Text;

namespace DriverMgr.DataAccess.Factory
{
    public abstract class DataFactory
    {
        public abstract T GetDAL<T>();
    }
}
