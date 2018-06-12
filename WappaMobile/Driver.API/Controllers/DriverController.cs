﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WappaMobile.Driver.API.Infrastructure.Repositories;
using WappaMobile.Driver.API.Model;

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
        public IActionResult CreateDriver([FromBody]DriverRegistry driverToAdd)
        {
            _driverRepository.Add(driverToAdd);

            return Ok(new { driverToAdd.Id });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDriver(string id, [FromBody]DriverRegistry driverToUpdate)
        {
            _driverRepository.Update(driverToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(string id)
        {
            _driverRepository.Delete(id);

            return NoContent();
        }
    }
}