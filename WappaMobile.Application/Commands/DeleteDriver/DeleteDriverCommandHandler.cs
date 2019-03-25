using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WappaMobile.Domain;
using WappaMobile.Persistence;

namespace WappaMobile.Application
{
    /// <summary>
    /// Handler for <see cref="DeleteDriverCommand"/>.
    /// </summary>
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand>
    {
        private readonly DriverContext _driverContext;

        public DeleteDriverCommandHandler(DriverContext driverContext)
        {
            _driverContext = driverContext ?? throw new System.ArgumentNullException(nameof(driverContext));
        }

        public async Task<Unit> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driverContext.Drivers.FindAsync(
                new object[] { request.DriverId }, cancellationToken
            );

            if (driver == null)
                throw new NotFoundException(nameof(Driver), request.DriverId.ToString());

            _driverContext.Drivers.Remove(driver);

            await _driverContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
