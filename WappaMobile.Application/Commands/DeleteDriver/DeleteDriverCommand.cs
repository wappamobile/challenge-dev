using System;
using MediatR;

namespace WappaMobile.Application
{
    /// <summary>
    /// Command to delete an existing driver.
    /// </summary>
    public class DeleteDriverCommand : IRequest
    {
        public Guid DriverId { get; }

        public DeleteDriverCommand(Guid driverId)
        {
            DriverId = driverId;
        }
    }
}
