using System;
using System.Net;
using System.Text.RegularExpressions;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using DriverCatalogService.Infrastructure;
using DriverCatalogService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DriverCatalogService.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriverCatalogQueryController : ControllerBase
    {
        private readonly IRepository _repository;

        public DriverCatalogQueryController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/drivers/all
        [HttpGet("all")]
        [LambdaSerializer(typeof(JsonSerializer))]
        public Driver[] Get([FromQuery] string sort_by)
        {
            var match = Regex.Match(sort_by, $"(asc|desc)\\(({nameof(Driver.Name.FirstName)}|{nameof(Driver.Name.LastName)})\\)");
            if (!match.Success)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return null;
            }

            var sortOrder = match.Groups[1].Value;
            var sortByField = match.Groups[2].Value;

            var drivers = _repository.List(sortByField, sortOrder);

            Response.StatusCode = (int) HttpStatusCode.OK;
            return drivers;
        }
    }
}