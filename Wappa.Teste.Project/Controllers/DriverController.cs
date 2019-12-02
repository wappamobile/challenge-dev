using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Wappa.Teste.Project.Core.Models;
using Wappa.Teste.Project.Core.Services;

namespace Wappa.Teste.Project.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] DriverModel driver)
        {
            DriverService.InsertDriver(driver);
        }

        [HttpPut("{id}")]
        public void Put([FromBody] DriverModel driver, int id)
        {
            driver.Id = id;
            DriverService.UpdateDriver(driver);
        }

        [HttpGet("{id}")]
        public DriverModel Get(int id)
        {
            return DriverService.GetDriver(id);
        }

        [HttpGet]
        public List<DriverModel> GetAll()
        {
            return DriverService.GetAllDrivers();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DriverService.DeleteDriver(id);
        }
    }
}