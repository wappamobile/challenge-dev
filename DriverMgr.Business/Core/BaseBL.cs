using DriverMgr.DataAccess.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverMgr.Business.Core
{
    public class BaseBL
    {
        internal DataFactory Factory { get; }

        public BaseBL(DataFactory factory)
        {
            Factory = factory;
        }
    }
}
