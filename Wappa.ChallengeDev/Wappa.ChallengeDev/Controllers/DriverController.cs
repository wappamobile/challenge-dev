using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wappa.Contracts;
using Wappa.Contracts.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wappa.ChallengeDev.Controllers
{
    /// <summary>
    /// Endpoint for Driver Management API
    /// </summary>
    [Route("api/v1/drivers")]
    public class DriverController 
    {
        public DriverController(IDriverDB driverDB, IGeoLocator geoLocator)
        {
            DriverDB = driverDB;
            GeoLocator = geoLocator;
        }

        public IDriverDB DriverDB { get; set; }
        
        public IGeoLocator GeoLocator { get; set; }

        /// <summary>
        /// Gets the drivers list, ordered by Last Name (ascending)
        /// </summary>
        /// <returns>(JSON) The ordered drivers list</returns>
        [HttpGet("list/last")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ContentResult GetDriversOrderedByLastName()
        {
            try
            {
                var response = DriverDB.GetDriversOrderByLastName();

                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(response, Formatting.Indented),
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = $"Internal Server Error: { ex.Message }",
                    ContentType = "text/plain",
                    StatusCode = 500
                };
            }
        }

        /// <summary>
        /// Gets the drivers list, ordered by First Name (ascending)
        /// </summary>
        /// <returns>(JSON) The ordered drivers list</returns>
        [HttpGet("list/first")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ContentResult GetDriversOrderedByFirstName()
        {
            try
            {
                var response = DriverDB.GetDriversOrderByFirstName();

                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(response, Formatting.Indented),
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = $"Internal Server Error: { ex.Message }",
                    ContentType = "text/plain",
                    StatusCode = 500
                };
            }
        }
              
        /// <summary>
        /// Save a new driver
        /// </summary>
        /// <param name="driver">(JSON) Driver</param>
        /// <returns>(String) Response Reason Phrase and message for the execution</returns>
        [HttpPost("add")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ContentResult> Post([FromBody]Driver driver)
        {
            try
            {  
                string address = $"{driver.Address.StreetName},{driver.Address.Number},{driver.Address.District},{driver.Address.City},{driver.Address.State},{driver.Address.Country}";
                var configuration = new ConfigurationBuilder()
                            .AddEnvironmentVariables()
                            .Build();
                driver.Address.Location = await GeoLocator.GetLocation(configuration, address);

                await DriverDB.SaveDriver(driver);

                return new ContentResult
                {
                    Content = "Created",
                    ContentType = "text/plain",
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = $"Bad Request: { ex.Message }",
                    ContentType = "text/plain",
                    StatusCode = 400
                };
            }
        }

        /// <summary>
        /// Update an existing driver, according to the "Id" field
        /// </summary>
        /// <param name="driver">(JSON) Driver, with the respective Id</param>
        /// <returns>(String) Response Reason Phrase and message for the execution</returns>
        [HttpPut("update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ContentResult> Put([FromBody]Driver driver)
        {
            try
            {
                string address = $"{driver.Address.StreetName},{driver.Address.Number},{driver.Address.District},{driver.Address.City},{driver.Address.State},{driver.Address.Country}";
                var configuration = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json")
                           .Build();
                driver.Address.Location = await GeoLocator.GetLocation(configuration, address);

                await DriverDB.UpdateDriver(driver);

                return new ContentResult
                {
                    Content = "OK",
                    ContentType = "text/plain",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = $"Bad Request: { ex.Message }",
                    ContentType = "text/plain",
                    StatusCode = 400
                };
            }
        }

        /// <summary>
        /// Update an existing driver, according to the "Id" field
        /// </summary>
        /// <param name="id">(String) The respective Id of the driver</param>
        /// <returns>(String) Response Reason Phrase and message for the execution</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ContentResult> Delete(string id)
        {
            try
            {
                await DriverDB.DeleteDriver(id);

                return new ContentResult
                {
                    Content = "OK",
                    ContentType = "text/plain",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = $"Bad Request: { ex.Message }",
                    ContentType = "text/plain",
                    StatusCode = 400
                };
            }
        }
    }
}
