using Wappa.Middleware.Application.Cars.Dto;
using DotNetCore.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wappa.Middleware.Application.Cars
{
    public interface ICarAppService
    {
        Task<List<CarDto>> GetAll();

        Task<IDataResult<int?>> Create(CreateOrEditCarDto input);

        Task<IResult> Update(CreateOrEditCarDto input);

        Task<IResult> Delete(int Id);

        Task<CarDto> GetById(int id);

        Task<CreateOrEditCarDto> GetForEdit(int id);

    }
}
