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
    public class ManufacturerController : ApiController
    {
        private ManufacturerBL _bl;

        public ManufacturerController()
        {
            var factory = DataFactoryProvider.Singleton.GetDataFactory();

            _bl = new ManufacturerBL(factory);
        }

        // GET api/values
        public IEnumerable<ManufacturerTO> Get()
        {
            return _bl.List();
        }
    }
}
