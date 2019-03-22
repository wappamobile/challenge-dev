using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriversManager.Core.Entities;
using DriversManager.Core;

namespace DriversManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversManagerController : ControllerBase
    {
        private readonly DriversManagerService service; 

        public DriversManagerController(DriversManagerService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Driver>> GetDrivers()
        {
            return service.Get();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Driver>> GetDriversOrderByName()
        {
            return service.GetOrderedByName();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Driver>> GetDriversOrderByLastName()
        {
            return service.GetOrderedByLastName();
        }

        [HttpGet("{id}")]
        public ActionResult<Driver> GetDriver(long id)
        {
            return service.Get(id.ToString());
        }

        [HttpPost]
        public ActionResult<Driver> Create(Driver driver)
        {
            service.Create(driver);

            return CreatedAtRoute("GetDriver", new { id = driver.Id.ToString() }, driver);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Driver driverIn)
        {
            var driver = service.Get(id);

            if (driver == null)
            {
                return NotFound();
            }

            service.Update(id, driverIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var driver = service.Get(id);

            if (driver == null)
            {
                return NotFound();
            }

            service.Remove(driver);

            return NoContent();
        }

    }
}