
using AutoMapper;
using Wappa.Middleware.Application.Drivers.Dto;
using Wappa.Middleware.Core.Drivers;
using Wappa.Middleware.Domain.Drivers;
using DotNetCore.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Middleware.Application.Drivers
{
    public class DriverAppService : WappaMiddlewareAplicationBase, IDriverAppService
    {
        private readonly IDriverManager _driverManager;

        public DriverAppService(IMapper objectMapper, IDriverManager driverManager)
            :base(objectMapper)
        {
            _driverManager = driverManager;
        }

        public async Task<List<DriverDto>> GetAll()
        {
            var drivers = _driverManager.Drivers
                .OrderBy(t => t.FirtName).ThenBy(n => n.LastName)
                .ToList();

            return _objectMapper.Map<List<DriverDto>>(drivers);
        }

        public async Task<DriverDto> GetById(int id)
        {
            return _objectMapper.Map<DriverDto>(await _driverManager.GetByIdAsync(id));
        }

        public async Task<CreateOrEditDriverDto> GetForEdit(int id)
        {
            return _objectMapper.Map<CreateOrEditDriverDto>(await _driverManager.GetByIdAsync(id));
        }

        public async Task<IDataResult<int?>> Create(CreateOrEditDriverDto input)
        {
            var driver = _objectMapper.Map<Driver>(input);

            var validation = new DriverValidatorCreate(_driverManager.Drivers.ToList()).Valid(driver);

            if (!validation.Success)
            {
                return ErrorDataResult<int?>(validation.Message);
            }
            
            return SuccessDataResult(await _driverManager.CreateAsync(driver));
        }

        public async Task<IResult> Update(CreateOrEditDriverDto input)
        {
            var driver = _objectMapper.Map<Driver>(input);

            var validation = new DriverValidatorUpdate(_driverManager.Drivers.ToList()).Valid(driver);

            if (!validation.Success)
            {
                return ErrorDataResult<int?>(validation.Message);
            }

            await _driverManager.UpdateAsync(driver);

            return SuccessResult();
        }

        public async Task<IResult> Delete(int Id)
        {
            var driver = await _driverManager.GetByIdAsync(Id);
            await _driverManager.DeleteAsync(driver);

            return SuccessResult();
        }
    }
}
