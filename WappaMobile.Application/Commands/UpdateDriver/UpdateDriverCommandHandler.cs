using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WappaMobile.Application.Services.Geocoding;
using WappaMobile.Domain;
using WappaMobile.Persistence;

namespace WappaMobile.Application
{
    /// <summary>
    /// Handler for <see cref="UpdateDriverCommand"/>.
    /// </summary>
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand>
    {
        private readonly DriverContext _driverContext;
        private readonly IMapper _mapper;
        private readonly IGeocoder _geocoder;

        public UpdateDriverCommandHandler(DriverContext driverContext, IMapper mapper, IGeocoder geocoder)
        {
            _driverContext = driverContext ?? throw new ArgumentNullException(nameof(driverContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _geocoder = geocoder ?? throw new ArgumentNullException(nameof(geocoder));
        }

        public async Task<Unit> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driverContext.Drivers.FindAsync(
                new object[] { request.DriverId }, cancellationToken
            );

            if (driver == null)
                throw new NotFoundException(nameof(Driver), request.DriverId.ToString());

            _mapper.Map(request.DriverDto, driver, typeof(ModifyDriverDto), typeof(Driver));

            driver.Address.Coordinates = await _geocoder.GetCoordinatesForAddress(driver.Address.ToString());

            await _driverContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
