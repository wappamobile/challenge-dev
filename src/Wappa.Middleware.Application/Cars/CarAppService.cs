using AutoMapper;
using DotNetCore.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Middleware.Application.Cars.Dto;
using Wappa.Middleware.Core.Cars;
using Wappa.Middleware.Core.Drivers;
using Wappa.Middleware.Domain.Cars;

namespace Wappa.Middleware.Application.Cars
{
    public class CarAppService : WappaMiddlewareAplicationBase, ICarAppService
    {
        private readonly ICarManager _carManager;
        private readonly IDriverManager _driverManager;

        public CarAppService(IMapper objectMapper, ICarManager carManager, IDriverManager driverManager)
        : base(objectMapper)
        {
            _carManager = carManager;
            _driverManager = driverManager;
        }

        public async Task<List<CarDto>> GetAll()
        {
            var cars = _carManager.Cars
                .OrderBy(t => t.Brand)
                .ToList();

            return _objectMapper.Map<List<CarDto>>(cars);
        }

        public async Task<CarDto> GetById(int id)
        {
            return _objectMapper.Map<CarDto>(await _carManager.GetByIdAsync(id));
        }

        public async Task<CreateOrEditCarDto> GetForEdit(int id)
        {
            return _objectMapper.Map<CreateOrEditCarDto>(await _carManager.GetByIdAsync(id));
        }

        public async Task<IDataResult<int?>> Create(CreateOrEditCarDto input)
        {
            var car = _objectMapper.Map<Car>(input);

            var validation = new CarValidatorCreate(_carManager.Cars.ToList()).Valid(car);

            if (!validation.Success)
            {
                return ErrorDataResult<int?>(validation.Message);
            }

            var driver = await _driverManager.GetByIdAsync(input.DriverId);

            var validationDriver = new CarValidatorDriver().Validate(driver);

            if (!validationDriver.IsValid)
            {
                return ErrorDataResult<int?>(validationDriver.Errors[0].ErrorMessage);
            }

            return SuccessDataResult(await _carManager.CreateAsync(car));
        }

        public async Task<IResult> Update(CreateOrEditCarDto input)
        {
            var car = _objectMapper.Map<Car>(input);

            var validation = new CarValidatorUpdate(_carManager.Cars.ToList()).Valid(car);

            if (!validation.Success)
            {
                return ErrorDataResult<int?>(validation.Message);
            }

            var driver = await _driverManager.GetByIdAsync(input.DriverId);

            var validationDriver = new CarValidatorDriver().Validate(driver);

            if (!validationDriver.IsValid)
            {
                return ErrorDataResult<int?>(validationDriver.Errors[0].ErrorMessage);
            }

            await _carManager.UpdateAsync(car);

            return SuccessResult();
        }

        public async Task<IResult> Delete(int Id)
        {
            var car = await _carManager.GetByIdAsync(Id);
            await _carManager.DeleteAsync(car);

            return SuccessResult();
        }
    }
}
