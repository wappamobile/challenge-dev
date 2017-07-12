using DriverMgr.Business.Validators;
using DriverMgr.DataAccess.Contracts;
using DriverMgr.DataAccess.Factory;
using DriverMgr.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverMgr.Business.Core
{
    public class DriverBL : BaseBL
    {
        private IDriverDAL _dal;

        public DriverBL(DataFactory factory) : base(factory)
        {
            _dal = Factory.GetDAL<IDriverDAL>();
        }

        public DriverTO Get(long id)
        {
            return _dal.Get(id);
        }

        public IEnumerable<DriverTO> List()
        {
            return _dal.List();
        }

        public void Create(DriverTO driver)
        {
            ValidateDriver(driver);

            _dal.Create(driver);
        }

        public void Update(DriverTO driver)
        {
            ValidateDriver(driver);

            _dal.Update(driver);
        }

        public void Delete(long id)
        {
            _dal.Delete(id);
        }

        private void ValidateDriver(DriverTO driver)
        {
            DriverValidator.From(driver).Validate();
        }
    }
}
