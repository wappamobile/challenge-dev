using System;
using MediatR;

namespace WappaMobile.Application
{
    /// <summary>
    /// Command to update an existing driver.
    /// </summary>
    public class UpdateDriverCommand : IRequest
    {
        public Guid DriverId { get; }
        public ModifyDriverDto DriverDto { get; }

        public UpdateDriverCommand(Guid driverId, ModifyDriverDto driverDto)
        {
            DriverId = driverId;
            DriverDto = driverDto ?? throw new ArgumentNullException(nameof(driverDto));
        }
    }
}
