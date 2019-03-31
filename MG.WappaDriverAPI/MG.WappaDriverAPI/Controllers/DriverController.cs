using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MG.WappaDriverAPI.Core.Services;
using MG.WappaDriverAPI.Core.Services.Interfaces;
using MG.WappaDriverAPI.TransientObjects;
using MG.WappaDriverAPI.TransientObjects.Responses;
using MG.WappaDriverAPI.TransientObjects.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MG.WappaDriverAPI.Controllers
{
    [Route("api/[controller]")]
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public DriverResponse Get(string id)
        {
            return _driverService.GetDriverById(id).ToDriverResponse();
        }

        [HttpGet("fulldriver/{id}")]
        public DriverResponse GetFullDriver(string id)
        {
            return _driverService.GetFullDriverById(id).ToDriverResponse();
        }

        // GET api/<controller>/Address/5
        [HttpGet("list/{name}")]
        public IEnumerable<DriverResponse> GetDriversByName(string name)
        {
            return _driverService.FindByName(name).Select(a => a.ToDriverResponse());
        }

        // POST api/<controller>
        [HttpPost]
        public DriverResponse Post(DriverRequest driver)
        {
            return _driverService.SaveOrUpdate(driver.ToDriver()).ToDriverResponse();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public DriverResponse Put(int id, DriverRequest driver)
        {
            return _driverService.SaveOrUpdate(driver.ToDriver()).ToDriverResponse();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _driverService.DeleteDriver(id);
        }

        // GET api/<controller>/Address/5
        [HttpGet("address/{id}")]
        public IEnumerable<AddressResponse> GetAddressesByDriverId(string id)
        {
            return _driverService.GetAddressesByDriverId(id).Select(a=>a.ToAddressResponse());
        }

        [HttpPost]
        [Route("address")]
        public AddressResponse PostAddress([FromBody]AddressRequest address)
        {
            return _driverService.SaveDriverAddress(address.DriverId, address.Name, address.Address).ToAddressResponse();
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("address/{id}")]
        public void DeleteAddress(string id)
        {
            _driverService.DeleteDriverAddress(id);
        }

        [HttpGet("colors")]
        public IEnumerable<string> GetColors()
        {
            return Enum.GetNames(typeof(System.Drawing.KnownColor));
        }
        
    }
}
