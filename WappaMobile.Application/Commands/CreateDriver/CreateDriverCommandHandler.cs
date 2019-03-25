using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WappaMobile.Domain;
using WappaMobile.Persistence;

namespace WappaMobile.Application
{
    /// <summary>
    /// Handler for <see cref="CreateDriverCommand"/>.
    /// </summary>
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand>
    {
        private readonly DriverContext _driverContext;
        private readonly IMapper _mapper;

        public CreateDriverCommandHandler(DriverContext driverContext, IMapper mapper)
        {
            _driverContext = driverContext ?? throw new ArgumentNullException(nameof(driverContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = _mapper.Map<Driver>(request.DriverDto);

            driver.Address.Coordinates = new Coordinates(10, 100);

            _driverContext.Drivers.Add(driver);

            await _driverContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
