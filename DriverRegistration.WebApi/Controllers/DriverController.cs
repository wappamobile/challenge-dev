using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverRegistration.Application;
using DriverRegistration.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DriverRegistration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        DriverApplication _driverApplication;

        public DriverController(DriverApplication driverApplication)
        {
            _driverApplication = driverApplication;
        }
        
        [HttpGet]
        public ActionResult<List<Driver>> GetAll(bool orderByDesc = false, bool byLastName = false)
        {
            return _driverApplication.GetAll(orderByDesc, byLastName);
        }
        
        [HttpGet("{id}")]
        public ActionResult<Driver> Get(string id)
        {
            return _driverApplication.Get(id);
        }
        
        [HttpPost]
        public void Post([FromBody] Driver driver)
        {
            _driverApplication.Insert(driver);
        }
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Driver driver)
        {
            _driverApplication.Update(id, driver);
        }
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _driverApplication.Delete(id);
        }
    }
}
