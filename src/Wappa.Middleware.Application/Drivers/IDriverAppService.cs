using Wappa.Middleware.Application.Drivers.Dto;
using DotNetCore.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wappa.Middleware.Application.Drivers
{
    public interface IDriverAppService
    {
        Task<List<DriverDto>> GetAll();

        Task<IDataResult<int?>> Create(CreateOrEditDriverDto input);

        Task<IResult> Update(CreateOrEditDriverDto input);

        Task<IResult> Delete(int Id);

        Task<DriverDto> GetById(int id);

        Task<CreateOrEditDriverDto> GetForEdit(int id);
    }
}
