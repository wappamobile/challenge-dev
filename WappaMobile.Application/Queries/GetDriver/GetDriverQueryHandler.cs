using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WappaMobile.Domain;
using WappaMobile.Persistence;

namespace WappaMobile.Application
{
    /// <summary>
    /// Query handler for <see cref="GetDriverQuery"/>.
    /// </summary>
    public class GetDriversQueryHandler : IRequestHandler<GetDriverQuery, ViewDriverDto>
    {
        private readonly DriverContext _driverContext;
        private readonly IMapper _mapper;

        public GetDriversQueryHandler(DriverContext driverContext, IMapper mapper)
        {
            _driverContext = driverContext ?? throw new System.ArgumentNullException(nameof(driverContext));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<ViewDriverDto> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            var driver = await _driverContext.Drivers.FindAsync(
                new object[] { request.DriverId }, cancellationToken
            );

            if (driver == null)
                throw new NotFoundException(nameof(Driver), request.DriverId.ToString());

            return _mapper.Map<ViewDriverDto>(driver);
        }
    }
}
