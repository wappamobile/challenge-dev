using DriverMgr.TO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverMgr.DataAccess.Contracts
{
    public interface IDriverDAL
    {
        DriverTO Get(long id);

        IEnumerable<DriverTO> List();

        void Create(DriverTO driver);

        void Update(DriverTO driver);

        void Delete(long id);
    }
}
