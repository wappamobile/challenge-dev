using Wappa.Middleware.Application.Cars;
using Wappa.Middleware.Application.Cars.Dto;
using Wappa.Middleware.Application.Drivers;
using Wappa.Middleware.Application.Drivers.Dto;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Wappa.Middleware.Test
{
    public class CarAppService_Tests : IClassFixture<TestClientProvider>
    {
        private readonly ICarAppService _carAppService;
        private readonly IDriverAppService _driverAppService;

        private ServiceProvider _serviceProvider;

        public CarAppService_Tests(TestClientProvider fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
            _carAppService = _serviceProvider.GetService<ICarAppService>();
            _driverAppService = _serviceProvider.GetService<IDriverAppService>();
        }

        [Fact]
        public async Task GetAll_Test()
        {
            var result = await _carAppService.GetAll();
            result.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Create_Update_And_Delete_Test()
        {
            //CREATE
            var resultId = await _driverAppService.Create(
                new CreateOrEditDriverDto
                {
                    Address = "Rua Mozelos",
                    Number = 389,
                    ZipCode = "02073100",
                    District = "Vila Paiva",
                    City = "Sao Pauo",
                    State = "SP",
                    Complement = "",
                    FirtName = "Joao",
                    LastName = "Silva"
                });

            var drivers = await _driverAppService.GetAll();
            drivers.Exists(t => t.FirtName == "Joao").ShouldBe(true);

            await _carAppService.Create(
                new CreateOrEditCarDto
                {
                    Brand = "Volks",
                    Model = "Fox Trend",
                    Plate = "ZZZ 1234",
                    DriverId = resultId.Data.Value
                });

            var cars = await _carAppService.GetAll();
                cars.Find(t => t.Plate == "ZZZ 1234").ShouldNotBe(null);

            var car = cars.Find(t => t.Plate == "ZZZ 1234");

            //EDIT
            var edit = await _carAppService.GetForEdit(car.Id);

            edit.Plate.ShouldBe("ZZZ 1234");

            edit.Plate = "YYY 3333";
            
            await _carAppService.Update(edit);

            cars = await _carAppService.GetAll();
            cars.Find(t => t.Plate == "YYY 3333").ShouldNotBe(null);

            //DELETE
            await _carAppService.Delete(edit.Id);

            cars = await _carAppService.GetAll();
            cars.Find(t => t.Plate == "YYY 3333").ShouldBe(null);

            await _driverAppService.Delete(resultId.Data.Value);

            drivers = await _driverAppService.GetAll();
            drivers.Find(t => t.FirtName == "Joao").ShouldBe(null);
        }
    }
}
