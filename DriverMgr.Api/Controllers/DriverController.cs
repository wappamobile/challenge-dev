using DriverMgr.Api.App_Start;
using DriverMgr.Business.Core;
using DriverMgr.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DriverMgr.Api.Controllers
{
    public class DriverController : ApiController
    {
        private DriverBL _bl;

        public DriverController()
        {
            var factory = DataFactoryProvider.Singleton.GetDataFactory();

            _bl = new DriverBL(factory);
        }

        // GET api/values
        public IEnumerable<DriverTO> Get()
        {
            return _bl.List();
        }

        // GET api/values/5
        public DriverTO Get(long id)
        {
            return _bl.Get(id);
        }

        // POST api/values
        public void Post([FromBody]DriverTO value)
        {
            _bl.Create(value);
        }

        // PUT api/values/5
        public void Put(long id, [FromBody]DriverTO value)
        {
            value.DriverId = id > 0 ? id : value.DriverId;

            _bl.Update(value);
        }

        // DELETE api/values/5
        public void Delete(long id)
        {
            _bl.Delete(id);
        }
    }
}
