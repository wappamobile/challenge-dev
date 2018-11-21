using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using ChallengeDev.Model;
using Domain.Enumerator;
using Domain.Model;
using Domain.Repository;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChallengeDev.Controllers
{
    /// <summary>
    /// Controller that manages the drivers' operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        readonly IDriverService driverService;
        readonly IGeocodingRepository geocodingRepository;
        readonly IGeocodingService geocodingService;
        readonly ChallengeDevEntityContext db = new ChallengeDevEntityContext();
        readonly string geocodingApiKey = Environment.GetEnvironmentVariable("GEOCODING_API_KEY");
        readonly IHttpClientFactory clientFactory;
        readonly ILoggerFactory logger;

        /// <summary>
        /// Service that manages the drivers' operations
        /// </summary>
        /// <param name="driverService">Dependency injection of IDriverService</param>
        /// <param name="geocodingRepository">Dependency injection of geocodingRepository</param>
        /// <param name="geocodingService">Dependency injection of IGeocodingService</param>
        /// <param name="clientFactory">Dependency injection of IHttpClientFactory</param>
        /// <param name="loggerFactory">Dependency injection of ILoggerFactory</param>
        public DriverController(IDriverService driverService, IGeocodingRepository geocodingRepository, IGeocodingService geocodingService, IHttpClientFactory clientFactory, ILoggerFactory loggerFactory)
        {
            this.driverService = driverService;
            this.geocodingRepository = geocodingRepository;
            this.geocodingService = geocodingService;
            this.clientFactory = clientFactory;
            this.logger = loggerFactory;
        }

        /// <summary>
        /// Retrieve all of the Drivers
        /// </summary>
        /// <param name="order">Order that the Drivers must be listed".</param>
        [HttpGet]
        public ResponseObject<ICollection<Driver>> Get([Required] DriverNameOrdenation order)
        {
            if (!ModelState.IsValid)
                return new ResponseObject<ICollection<Driver>> { Message = "Invalid Params", Response = new List<Driver>() };

            var result = driverService.List(logger, db, order);
            return new ResponseObject<ICollection<Driver>>() { Message = $"Drivers Returned: {result.Count}", Response = result };
        }

        /// <summary>
        /// Register a Driver
        /// </summary>
        /// <param name="firstName">Driver's first name".</param>
        /// <param name="lastName">Driver's last name".</param>
        /// <param name="brand">Car's brand name".</param>
        /// <param name="model">Car's model name".</param>
        /// <param name="plate">Car's plate".</param>
        /// <param name="country">Adress' country</param>
        /// <param name="street">Address' street name".</param>
        /// <param name="number">Address' number".</param>
        /// <param name="complement">Address' complement".</param>
        /// <param name="neighborhood">Address' neighborhood name".</param>
        /// <param name="city">Address' city name".</param>
        /// <param name="state">Address' state name".</param>
        /// <param name="zipCode">Address' zip code".</param>
        [HttpPost]
        public ResponseObject<bool> Post([MinLength(2)][MaxLength(50)][Required]string firstName,
                        [MinLength(2)][MaxLength(50)][Required]string lastName,
                        [MinLength(3)][MaxLength(50)][Required]string brand,
                        [MinLength(2)][MaxLength(50)][Required]string model,
                        [MinLength(8)][MaxLength(8)][Required]string plate,
                        [MinLength(3)][MaxLength(50)][Required]string country,
                        [MinLength(5)][MaxLength(255)][Required]string street,
                        [Required]int number,
                        [MinLength(3)][MaxLength(50)]string complement,
                        [MinLength(3)][MaxLength(50)][Required]string neighborhood,
                        [MinLength(3)][MaxLength(50)][Required]string city,
                        [MinLength(2)][MaxLength(50)][Required]string state,
                        [MinLength(9)][MaxLength(9)][Required]string zipCode)
        {
            if (!ModelState.IsValid)
                return new ResponseObject<bool> { Message = "Invalid Params", Response = false };

            var address = new Address()
            {
                Country = country,
                Street = street,
                Number = number,
                Complement = complement,
                Neighborhood = neighborhood,
                City = city,
                State = state,
                ZipCode = zipCode
            };

            var car = new Car()
            {
                Brand = brand,
                Model = model,
                Plate = plate.ToUpper()
            };

            var driver = new Driver()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Car = car
            };

            var result = driverService.Save(logger, db, clientFactory, geocodingRepository, geocodingService, geocodingApiKey, driver);

            return new ResponseObject<bool> { Message = result, Response = true };
        }

        /// <summary>
        /// Edit a Driver's data 
        /// </summary>
        /// <param name="id">Driver's ID".</param>
        /// <param name="firstName">Driver's first name".</param>
        /// <param name="lastName">Driver's last name".</param>
        /// <param name="brand">Car's brand name".</param>
        /// <param name="model">Car's model name".</param>
        /// <param name="plate">Car's plate".</param>
        /// <param name="country">Adress' country</param>
        /// <param name="street">Address' street name".</param>
        /// <param name="number">Address' number".</param>
        /// <param name="complement">Address' complement".</param>
        /// <param name="neighborhood">Address' neighborhood name".</param>
        /// <param name="city">Address' city name".</param>
        /// <param name="state">Address' state name".</param>
        /// <param name="zipCode">Address' zip code".</param>
        [HttpPut]
        public ResponseObject<bool> Put([Required]int id,
                        [MinLength(2)][MaxLength(50)][Required]string firstName,
                        [MinLength(2)][MaxLength(50)][Required]string lastName,
                        [MinLength(3)][MaxLength(50)][Required]string brand,
                        [MinLength(2)][MaxLength(50)][Required]string model,
                        [MinLength(8)][MaxLength(8)][Required]string plate,
                        [MinLength(3)][MaxLength(50)][Required]string country,
                        [MinLength(5)][MaxLength(255)][Required]string street,
                        [Required]int number,
                        [MinLength(3)][MaxLength(50)]string complement,
                        [MinLength(3)][MaxLength(50)][Required]string neighborhood,
                        [MinLength(3)][MaxLength(50)][Required]string city,
                        [MinLength(2)][MaxLength(50)][Required]string state,
                        [MinLength(9)][MaxLength(9)][Required]string zipCode)
        {
            if (!ModelState.IsValid)
                return new ResponseObject<bool> { Message = "Invalid Params", Response = false };

            var address = new Address()
            {
                Country = country,
                Street = street,
                Number = number,
                Complement = complement,
                Neighborhood = neighborhood,
                City = city,
                State = state,
                ZipCode = zipCode
            };

            var car = new Car()
            {
                Brand = brand,
                Model = model,
                Plate = plate
            };

            var driver = new Driver()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Car = car
            };

            var result = driverService.Update(logger, db, clientFactory, geocodingRepository, geocodingService, geocodingApiKey, driver);
            return new ResponseObject<bool> { Message = result, Response = true };
        }


        /// <summary>
        /// Delete a Driver
        /// </summary>
        /// <param name="id">Driver's ID".</param>
        [HttpDelete]
        public ResponseObject<bool> Delete([Required]int id)
        {
            if (!ModelState.IsValid)
                return new ResponseObject<bool> { Message = "Invalid Params", Response = false };

            var result = driverService.Delete(logger, db, id);
            return new ResponseObject<bool> { Message = result, Response = false };
        }
    }
}
