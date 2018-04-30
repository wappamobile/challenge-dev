using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Wappa.Challenge.Api;
using Wappa.Challenge.Domain.Commands.Inputs;
using Wappa.Challenge.Domain.Handlers;
using Wappa.Challenge.Infrastructure.Repositories;
using Wappa.Challenge.Services;
using Wappa.Challenge.Shared.Configuration;
using Xunit;

namespace Wappa.Challenge.UnitTest
{
    public class DriverHandlerTest
    {
        private readonly DriverHandler _handler;

        public DriverHandlerTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var db = new MongoClient(config.GetConnectionString("Wappa")).GetDatabase("Wappa");
            var googleConfiguration = config.GetSection("GoogleMapsConfiguration").Get<GoogleMapsConfiguration>();

            var googleService = new GoogleService(googleConfiguration);
            var repository = new DriverRepository(db);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = new Mapper(mapperConfiguration);

            _handler = new DriverHandler(repository, mapper, googleService);
        }

        [Fact]
        public void Insert()
        {
            var command = new CreateDriverCommand
            {
                FirstName = "Felipe",
                LastName = "Jesus",
                Address = new AddressCommand
                {
                    Street = "Av Fabio Eduardo Ramos Esquivel",
                    Number = "2900",
                    Complement = "Torre 7 Ap 145",
                    Neighborhood = "Canhema",
                    City = "Diadema",
                    State = "São Paulo",
                    Country = "Brasil",
                    ZipCode = "09941-202"
                },
                Car = new CarCommand
                {
                    Manufacturer = "Honda",
                    Model = "FIT",
                    LicensePlate = "ENW-9465"
                }
            };

            var result = _handler.Handle(command);
            Assert.True(result.Success);
        }

        [Fact]
        public void Update()
        {
            var command = new UpdateDriverCommand()
            {
                Id = Guid.NewGuid(),
                FirstName = "Felipe",
                LastName = "Jesus",
                Address = new AddressCommand
                {
                    Street = "Av Fabio Eduardo Ramos Esquivel",
                    Number = "2900",
                    Complement = "Torre 7 Ap 145",
                    Neighborhood = "Canhema",
                    City = "Diadema",
                    State = "São Paulo",
                    Country = "Brasil",
                    ZipCode = "09941-202"
                },
                Car = new CarCommand
                {
                    Manufacturer = "Honda",
                    Model = "FIT",
                    LicensePlate = "ENW-9465"
                }
            };

            var result = _handler.Handle(command);
            Assert.True(!result.Success);
        }

        [Fact]
        public void Delete()
        {
            var command = new DeleteDriverCommand
            {
                Id = Guid.NewGuid()
            };

            var result = _handler.Handle(command);
            Assert.True(!result.Success);
        }

        [Fact]
        public void List()
        {
            var command = new ListDriversCommand()
            {
                OrderBy = OrderByOptionCommand.Firstname
            };

            var result = _handler.Handle(command);
            Assert.True(result.Success);
        }
    }
}