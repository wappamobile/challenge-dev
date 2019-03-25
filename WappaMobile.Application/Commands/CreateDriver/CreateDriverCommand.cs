using System;
using MediatR;

namespace WappaMobile.Application
{
    /// <summary>
    /// Command to create a new driver.
    /// </summary>
    public class CreateDriverCommand : IRequest
    {
        public ModifyDriverDto DriverDto { get; }

        public CreateDriverCommand(ModifyDriverDto driverDto)
        {
            DriverDto = driverDto ?? throw new ArgumentNullException(nameof(driverDto));
        }
    }
}
