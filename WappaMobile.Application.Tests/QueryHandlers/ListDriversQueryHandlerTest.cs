using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WappaMobile.Domain;
using WappaMobile.Persistence;
using Xunit;

namespace WappaMobile.Application.Tests
{
    public class ListDriversQueryHandlerTest : IDisposable
    {
        private readonly DriverContext _driverContext;
        private readonly Mapper _mapper;

        public ListDriversQueryHandlerTest()
        {
            var rnd = new Random();
            _driverContext = new DriverContext(new DbContextOptionsBuilder<DriverContext>()
                        .UseInMemoryDatabase("Test" + rnd.NextDouble().ToString())
                        .Options);
            _driverContext.Drivers.Add(new Driver
            {
                FirstName = "Raphael",
                LastName = "de Abreu",
                Car = new Car(),
                Address = new Address()
            });
            _driverContext.Drivers.Add(new Driver
            {
                FirstName = "Aline",
                LastName = "Maia",
                Car = new Car(),
                Address = new Address()
            });
            _driverContext.SaveChanges();

            _mapper = new Mapper(new MapperConfiguration(
                            cfg => cfg.AddProfile(new DomainProfile())));
        }

        public void Dispose()
        {
            _driverContext.Dispose();
        }

        [Fact]
        public async Task Handle_WithFirstNameSorting_ReturnsInCorrectOrder()
        {
            // Arrange
            var handler = new ListDriversQueryHandler(_driverContext, _mapper);
            var command = new ListDriversQuery(ListDriversQuery.Sorting.FirstName);

            // Act
            var results = (await handler.Handle(command, CancellationToken.None)).ToArray();

            // Assert
            Debug.WriteLine(results[0].FirstName);

            Assert.Equal("Aline", results[0].FirstName);
            Assert.Equal("Raphael", results[1].FirstName);
        }

        [Fact]
        public async Task Handle_WithLastNameSorting_ReturnsInCorrectOrder()
        {
            // Arrange
            var handler = new ListDriversQueryHandler(_driverContext, _mapper);
            var command = new ListDriversQuery(ListDriversQuery.Sorting.LastName);

            // Act
            var results = (await handler.Handle(command, CancellationToken.None)).ToArray();

            // Assert
            Debug.WriteLine(results[0].FirstName);

            Assert.Equal("de Abreu", results[0].LastName);
            Assert.Equal("Maia", results[1].LastName);
        }
    }
}
