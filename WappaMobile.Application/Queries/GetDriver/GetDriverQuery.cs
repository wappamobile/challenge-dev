using System;
using MediatR;

namespace WappaMobile.Application
{
    /// <summary>
    /// Query to retrieve a signle driver.
    /// </summary>
    public class GetDriverQuery : IRequest<ViewDriverDto>
    {
        public Guid DriverId { get; }

        public GetDriverQuery(Guid driverId)
        {
            DriverId = driverId;
        }
    }
}
