using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using DriverCatalogService.Infrastructure;
using DriverCatalogService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace DriverCatalogService.Controllers
{
    [Route("api/driver")]
    [ApiController]
    public class DriverCatalogController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly DriverValidator _validator;

        public DriverCatalogController(IRepository repository, DriverValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        // POST api/driver
        [HttpPost]
        [LambdaSerializer(typeof(JsonSerializer))]
        public object Post([FromBody] Driver driver)
        {
            var validationErrors = _validator.ValidateForCreation(driver);
            if (validationErrors.Any())
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new ErrorResponse { Errors = validationErrors.ToArray() };
            }

            driver.Id = Guid.NewGuid().ToString();
            driver.CreatedAt = DateTime.UtcNow;

            _repository.Save(driver);

            Response.StatusCode = (int) HttpStatusCode.Created;
            Response.Headers["Location"] = Request.Path.Add($"/{driver.Id}").Value;
            return driver.Id;
        }

        // GET api/driver/{id}
        [HttpGet("{id}")]
        [LambdaSerializer(typeof(JsonSerializer))]
        public Driver Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Missing required parameter id");
            }

            var driver = _repository.Load(id);
            if (driver == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            Response.StatusCode = (int) HttpStatusCode.OK;
            return driver;
        }

        // PUT api/driver
        [HttpPut]
        [LambdaSerializer(typeof(JsonSerializer))]
        public object Put([FromBody] Driver driver)
        {
            var validationErrors = _validator.ValidateFields(driver);
            if (validationErrors.Any())
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new ErrorResponse { Errors = validationErrors.ToArray() };
            }

            var target = _repository.Load(driver.Id);
            if (target == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return new ErrorResponse { Errors = new []{ new Error { Problem = "Not found", Where = nameof(Driver)} }};
            }

            target.Name.FirstName = driver.Name.FirstName;
            target.Name.LastName = driver.Name.LastName;
            target.ModifiedAt = DateTime.UtcNow;

            _repository.Save(target);

            Response.StatusCode = (int) HttpStatusCode.NoContent;
            return null;
        }
        
        // DELETE api/driver/{id}
        [HttpDelete("{id}")]
        [LambdaSerializer(typeof(JsonSerializer))]
        public object Delete(string id)
        {
            var target = _repository.Load(id);
            if (target == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return new ErrorResponse { Errors = new []{ new Error { Problem = "Not found", Where = nameof(Driver)} }};
            }

            _repository.Delete(id);

            Response.StatusCode = (int) HttpStatusCode.NoContent;
            return null;
        }
    }
}