using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WappaMobile.Driver.API.Infrastructure.Repositories;
using WappaMobile.Driver.API.Model;
using WappaMobile.Driver.API.ViewModel;

namespace WappaMobile.Driver.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class DriverController : Controller
    {
        private readonly IDriverRepository _driverRepository;

        public DriverController(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
        }

        [HttpGet]
        public IActionResult GetDriver()
        {
            return Ok(_driverRepository.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetDriver(string id)
        {
            return Ok(_driverRepository.Get(id));
        }

        [HttpPost]
        public IActionResult CreateDriver([FromBody]DriverRequest driver)
        {
            var driverToAdd = new DriverRegistry
            {
                Name = driver.Name,
                Vehicle = driver.Vehicle,
                Address = driver.Address
            };

            _driverRepository.Add(driverToAdd);

            //For some reason, CreatedAtAction acts weird when I remove the 'value' parameter. It returns a wrong route to the 'location' header.
            return CreatedAtAction(nameof(GetDriver), new { id = driverToAdd.Id }, driverToAdd);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDriver(string id, [FromBody]DriverRequest driver)
        {
            var driverToUpdate = new DriverRegistry
            {
                Id = id,
                Name = driver.Name,
                Vehicle = driver.Vehicle,
                Address = driver.Address
            };

            _driverRepository.Update(driverToUpdate);

            //Here CreatedAtAction acts normal with or without the 'value' parameter. But I will keep it here for the sake of consistency.
            return CreatedAtAction(nameof(GetDriver), new { id = driverToUpdate.Id }, driverToUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(string id)
        {
            _driverRepository.Delete(id);

            return NoContent();
        }
    }
}