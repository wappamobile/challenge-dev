using System.IO;
using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using DriverCatalogService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DriverCatalogService.Controllers
{
    [Route("api/driver")]
    [ApiController]
    public class DriverCatalogController : ControllerBase
    {
        // POST api/driver
        [HttpPost]
        [LambdaSerializer(typeof(JsonSerializer))]
        public void Post([FromBody] Driver driver)
        {
            Response.StatusCode = (int) HttpStatusCode.Created;
            Response.Headers["Location"] = Request.Path.Add("/1").Value;
        }
    }
}