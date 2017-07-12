using DriverMgr.Api.App_Start;
using DriverMgr.DataAccess.Factory;
using DriverMgr.DataAccessMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverMgr.Api.Tests.Global
{
    public class DataFactoryProviderMock : DataFactoryProvider
    {
        public DataFactory Factory { get; set; }

        public override DataFactory GetDataFactory()
        {
            return Factory;
        }
    }
}
